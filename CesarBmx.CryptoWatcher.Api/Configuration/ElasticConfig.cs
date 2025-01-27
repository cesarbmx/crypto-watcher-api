

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class ElasticConfig
    {
        public static IServiceCollection ConfigureElastic(this IServiceCollection services)
        {
            services.AddAllElasticApm();

            return services;
        }
    }
}
