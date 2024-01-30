using CesarBmx.Shared.Api.Configuration;
using CesarBmx.Shared.Health.HealthChecks;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class HealthConfig
    {
        public static IServiceCollection ConfigureHealth(this IServiceCollection services, IConfiguration configuration)
        {
            // Shared
            services.ConfigureSharedHealth(configuration);

            services.AddHealthChecks()
               .AddCheck<CoinpaprikaHealthCheck>("Coinpaprika API");

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
