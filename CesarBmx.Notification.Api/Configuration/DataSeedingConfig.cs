using CesarBmx.Notification.Persistence.Contexts;
using CesarBmx.Shared.Api.Configuration;

namespace CesarBmx.Notification.Api.Configuration
{
    public static class DataSeedingConfig
    {
        public static IServiceCollection ConfigureDataSeeding(this IServiceCollection services)
        {
            services.ConfigureSharedDataSeeding<MainDbContext>();

            return services;
        }
    }
}
