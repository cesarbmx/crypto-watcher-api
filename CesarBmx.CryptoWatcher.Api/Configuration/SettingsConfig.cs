using CesarBmx.Shared.Api.Configuration;
using CesarBmx.CryptoWatcher.Application.Settings;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class SettingsConfig
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddConfiguration<AppSettings>(configuration);
            services.ConfigureSharedSettings(configuration);

            return services;
        }
    }
}