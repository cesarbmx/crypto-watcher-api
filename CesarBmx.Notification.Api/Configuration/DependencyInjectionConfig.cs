using CesarBmx.Notification.Application.Jobs;
using CesarBmx.Notification.Application.Services;
using CesarBmx.Notification.Application.Settings;
using CesarBmx.Notification.Persistence.Contexts;
using CesarBmx.Shared.Api.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CesarBmx.Notification.Api.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Grab settings
            var appSettings = configuration.GetSection<AppSettings>();

            //Db contexts
            if (appSettings.UseMemoryStorage)
            {
                services.AddDbContext<MainDbContext, MainDbContext>(options => options
                     .UseInMemoryDatabase(appSettings.DatabaseName)
                     .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }
            else
            {
                services.AddDbContext<MainDbContext, MainDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString(appSettings.DatabaseName))
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
            services.AddScoped<ScriptVariablesService>();

            // Jobs
            services.AddScoped<MainJob>();
            services.AddScoped<SendWhatsappNotificationsJob>();
            services.AddScoped<SendTelgramNotificationsJob>();
            services.AddScoped<RemoveObsoleteLinesJob>();

            // API clients
            services.AddScoped<CoinpaprikaAPI.Client, CoinpaprikaAPI.Client>();

            // Open telemetry
            services.AddSingleton(x=> new ActivitySource(appSettings.ApplicationId));

            // Return
            return services;
        }
    }
}
