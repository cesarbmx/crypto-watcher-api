using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api2.Configuration
{
    public static class CachingConfig
    {
        public static IServiceCollection ConfigureCaching(this IServiceCollection services)
        {
            //services.AddDistributedMemoryCache();
            //services.AddEnyimMemcached();

            return services;
        }
    }
}
