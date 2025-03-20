using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Shared.Swagger
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, string serviceName)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"{serviceName} API",
                    Version = "v1"
                });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app, string serviceName)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{serviceName} API v1");
            });
        }
    }
}
