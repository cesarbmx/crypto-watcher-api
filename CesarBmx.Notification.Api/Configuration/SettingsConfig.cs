using CesarBmx.Shared.Api.Configuration;
using CesarBmx.Notification.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.Notification.Api.Configuration
{
    public static class SettingsConfig
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfiguration<AppSettings>(configuration);
            services.ConfigureSharedSettings(configuration);

            return services;
        }
    }
}