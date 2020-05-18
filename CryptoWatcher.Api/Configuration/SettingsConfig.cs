using CesarBmx.Shared.Api.Configuration;
using CryptoWatcher.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class SettingsConfig
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfiguration<AppSettings>(configuration, "AppSettings");

            return services;
        }
    }
}