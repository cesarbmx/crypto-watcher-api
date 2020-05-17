using CesarBmx.Shared.Api.Configuration;
using CesarBmx.Shared.Api.Helpers;
using CesarBmx.Shared.Common.Providers;
using CryptoWatcher.Application.Jobs;
using CryptoWatcher.Application.Services;
using CryptoWatcher.Domain.Models;
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
            //Contexts
            if (bool.Parse(configuration["AppSettings:UseMemoryStorage"]))
            {
                services.AddDbContext<DbContext, MainDbContext>(options => options
                     .UseInMemoryDatabase("CryptoWatcher")
                     .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }
            else
            {
                services.AddDbContext<DbContext, MainDbContext>(options => options
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
            services.AddScoped<LineChartService>();
            services.AddScoped<ScriptVariableSetService>();

            // Audit repositories
            services.AddAudit();
            services.AddAudit<Currency>();
            services.AddAudit<Watcher>();
            services.AddAudit<User>();
            services.AddAudit<Notification>();
            services.AddAudit<Order>();
            services.AddAudit<Indicator>();
            services.AddAudit<Line>();
            services.AddAudit<IndicatorDependency>();
            services.AddAudit<Line>();
            
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
