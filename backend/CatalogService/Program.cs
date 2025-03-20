using CatalogService.Domain.Interfaces;
using CatalogService.Infrastructure.Persistence;
using CatalogService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.ErrorHandling;
using Shared.HealthChecks;
using Shared.Logging;
using Shared.Resilience;
using Shared.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

// Add PostgreSQL database
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDb")));

// Register repositories
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddSwaggerConfiguration("CatalogService");

// Add Health Checks
builder.Services.AddHealthChecks().AddCheck<HealthCheckService>("catalog_service_health");

// Add resilience policies (retry, circuit breaker)
builder.Services.AddHttpClient("default")
    .AddPolicyHandler(ResiliencePolicies.GetRetryPolicy())
    .AddPolicyHandler(ResiliencePolicies.GetCircuitBreakerPolicy());

var app = builder.Build();

// Use middleware
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerConfiguration("CatalogService");

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
