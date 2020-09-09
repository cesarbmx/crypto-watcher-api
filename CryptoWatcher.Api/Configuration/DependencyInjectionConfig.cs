using CesarBmx.Shared.Api.Helpers;
using CesarBmx.Shared.Common.Providers;
using CryptoWatcher.Application.Jobs;
using CryptoWatcher.Application.Services;
using CryptoWatcher.Application.Settings;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Grab AppSettings
            var appSettings = new AppSettings();
            configuration.GetSection("AppSettings").Bind(appSettings);

            //Contexts
            if (appSettings.UseMemoryStorage)
            {
                services.AddDbContext<MainDbContext, MainDbContext>(options => options
                     .UseInMemoryDatabase("CryptoWatcher")
                     .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }
            else
            {
                services.AddDbContext<MainDbContext, MainDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("CryptoWatcher"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }

            // Services
            services.AddScoped<CurrencyService>();
            services.AddScoped<WatcherService>();
            services.AddScoped<UserService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<OrderService>();
            services.AddScoped<IndicatorService>();
            services.AddScoped<LineService>();
            services.AddScoped<ChartService>();
            services.AddScoped<ScriptVariableSetService>();

            // Jobs
            services.AddScoped<MainJob>();
            services.AddScoped<SendWhatsappNotificationsJob>();
            services.AddScoped<SendTelgramNotificationsJob>();
            services.AddScoped<RemoveObsoleteLinesJob>();

            // Other
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddHttpContextAccessor();
            services.AddScoped<CoinpaprikaAPI.Client, CoinpaprikaAPI.Client>();

            return services;
        }
    }
}
