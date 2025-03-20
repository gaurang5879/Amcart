using AuthService.Application.Services;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.ErrorHandling;
using Shared.HealthChecks;
using Shared.Logging;
using Shared.Resilience;
using Shared.Swagger;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

// Add PostgreSQL database
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthDb")));

// Add Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Register repositories and services
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddSwaggerConfiguration("AuthService");

// Add Health Checks
builder.Services.AddHealthChecks().AddCheck<HealthCheckService>("auth_service_health");

// Add resilience policies (retry, circuit breaker)
builder.Services.AddHttpClient("default")
    .AddPolicyHandler(ResiliencePolicies.GetRetryPolicy())
    .AddPolicyHandler(ResiliencePolicies.GetCircuitBreakerPolicy());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("https://agreeable-flower-0001d7f0f.6.azurestaticapps.net/")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Use middleware
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerConfiguration("AuthService");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.UseCors("AllowReactApp");

app.Run();
