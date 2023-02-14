using CesarBmx.CryptoWatcher.Persistence.Contexts;
using CesarBmx.Shared.Api.Configuration;

namespace CesarBmx.CryptoWatcher.Api.Configuration
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
