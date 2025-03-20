using SearchService.Infrastructure.AzureSearch;
using SearchService.Domain.Interfaces;
using Microsoft.OpenApi.Models;
using Shared.Logging;
using Shared.ErrorHandling;
using Shared.HealthChecks;
using Shared.Swagger;
using Shared.Resilience;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

// Register Azure Search Service
builder.Services.AddScoped<ISearchService, AzureSearchService>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddSwaggerConfiguration("SearchService");

// Add Health Checks
builder.Services.AddHealthChecks().AddCheck<HealthCheckService>("search_service_health");

// Add resilience policies
builder.Services.AddHttpClient("default")
    .AddPolicyHandler(ResiliencePolicies.GetRetryPolicy())
    .AddPolicyHandler(ResiliencePolicies.GetCircuitBreakerPolicy());

var app = builder.Build();

// Use middleware
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerConfiguration("SearchService");

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
