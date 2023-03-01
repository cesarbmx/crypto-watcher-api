using CesarBmx.Notification.Application.Jobs;
using CesarBmx.Notification.Application.Services;
using CesarBmx.Notification.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CesarBmx.Notification.Application.Settings;

namespace CesarBmx.Notification.Service.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Grab settings
            var appSettings = configuration.GetSection<AppSettings>();

            //Contexts
            if (bool.Parse(configuration["AppSettings:UseMemoryStorage"]))
            {
                services.AddDbContext<DbContext, MainDbContext>(options => options
                     .UseInMemoryDatabase(appSettings.da)
                     .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }
            else
            {
                services.AddDbContext<DbContext, MainDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("Notification"))
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

            // Other
            services.AddScoped<CoinpaprikaAPI.Client, CoinpaprikaAPI.Client>();

            return services;
        }
    }
}
