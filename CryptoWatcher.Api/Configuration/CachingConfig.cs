using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
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
