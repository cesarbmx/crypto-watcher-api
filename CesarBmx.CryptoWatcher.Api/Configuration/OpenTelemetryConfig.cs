using System.Reflection;
using CesarBmx.Shared.Api.Configuration;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class OpenTelemetryConfig
    {
        public static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSharedOpenTelemetry(configuration, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
