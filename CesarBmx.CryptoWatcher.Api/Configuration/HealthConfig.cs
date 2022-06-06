using CesarBmx.CryptoWatcher.Api.HealthChecks;
using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class HealthConfig
    {
        public static IServiceCollection ConfigureHealth(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSharedHealth(configuration)
                .AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("CryptoWatcher"))
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
