using CesarBmx.Shared.Api.Configuration;
using CesarBmx.Notification.Persistence.Contexts;
using CesarBmx.Notification.Application.Consumers;

namespace CesarBmx.Notification.Api.Configuration
{
    public static class MasstransitConfig
    {
        public static IServiceCollection ConfigureMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            // Shared
            services.ConfigureSharedMasstransit<MainDbContext, OrderAddedConsumer>(configuration);

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
