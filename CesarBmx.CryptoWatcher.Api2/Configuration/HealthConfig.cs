using CesarBmx.CryptoWatcher.Api2.HealthChecks;
using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api2.Configuration
{
    public static class HealthConfig
    {
        public static IServiceCollection ConfigureHealth(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSharedHealth(configuration)
                .AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("CryptoWatcher"), null, "SQL Server")
                .AddCheck<CoinpaprikaHealthCheck>("Coinpaprika API");
            // Add your health checks

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
