using CesarBmx.Shared.Api.Configuration;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class LoggingConfig
    {
        public static IServiceCollection ConfigureLogging(this IServiceCollection services)
        {
            services.ConfigureSharedLogging();

            return services;
        }
    }
}
