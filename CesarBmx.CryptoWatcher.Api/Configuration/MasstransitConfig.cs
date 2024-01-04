using CesarBmx.Shared.Api.Configuration;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using CesarBmx.CryptoWatcher.Application.Consumers;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class MasstransitConfig
    {
        public static IServiceCollection ConfigureMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            // Shared
            services.ConfigureSharedMasstransit<MainDbContext>(configuration, typeof(ConfirmOrderConsumer));

            // Return
            return services;
        }
        public static IApplicationBuilder ConfigureMasstransit(this IApplicationBuilder app)
        {
            // Shared
            app.ConfigureSharedMasstransit();

            return app;
        }
    }
}
