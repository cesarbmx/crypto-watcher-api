using CesarBmx.CryptoWatcher.Application.Jobs;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.CryptoWatcher.Application.Settings;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using CesarBmx.Shared.Api.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace CesarBmx.CryptoWatcher.Api.Configuration
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
            services.AddScoped<TestService>();

            // Jobs
            services.AddScoped<MainJob>();
            services.AddScoped<SendWhatsappNotificationsJob>();
            services.AddScoped<SendTelgramNotificationsJob>();
            services.AddScoped<RemoveObsoleteLinesJob>();

            // API clients
            services.AddScoped<CoinpaprikaAPI.Client, CoinpaprikaAPI.Client>();

            // Shared
            services.AddOpenTelemetry();
            services.AddLogExecutionTime();

            // Return
            return services;
        }
    }
}
