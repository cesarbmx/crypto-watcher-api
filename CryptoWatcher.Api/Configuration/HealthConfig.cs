using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class HealthConfig
    {
        public static IServiceCollection ConfigureHealth(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSharedHealth(configuration)
                .AddHealthChecks();
            // Add your health checks
            //.AddMySql(configuration.GetConnectionString("MainDb"), "MySql connection");

            // Return
            return services;
        }
        public static IApplicationBuilder ConfigureHealth(this IApplicationBuilder app)
        {
            app.ConfigureSharedHealth();

            // Return
            return app;
        }
    }
}
