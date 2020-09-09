using CesarBmx.Shared.Api.Configuration;
using CryptoWatcher.Application.Jobs;
using CryptoWatcher.Application.Services;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Service.Configuration
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
            services.AddScoped<ChartService>();
            services.AddScoped<ScriptVariableSetService>();

            // Repositories
            services.AddRepository<Currency>();
            services.AddRepository<Watcher>();
            services.AddRepository<User>();
            services.AddRepository<Notification>();
            services.AddRepository<Order>();
            services.AddRepository<Indicator>();
            services.AddRepository<Line>();
            services.AddRepository<IndicatorDependency>();
            services.AddRepository<Line>();

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
