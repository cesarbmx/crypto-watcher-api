using CoinMarketCap;
using CoinMarketCap.Core;
using CryptoWatcher.Application.Services;
using CryptoWatcher.BackgroundJobs;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Web.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // UseMemoryStorage
            if (bool.Parse(configuration["AppSettings:UseMemoryStorage"]))
            {
                //Contexts (UOW)
                services.AddDbContext<MainDbContext>(options => options
                    .UseInMemoryDatabase("CryptoWatcher")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }
            else
            {
                //Contexts (UOW)
                services.AddDbContext<MainDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("CryptoWatcher"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }

            // Services
            services.AddScoped<CurrencyService, CurrencyService>();
            services.AddScoped<WatcherService, WatcherService>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<NotificationService, NotificationService>();
            services.AddScoped<OrderService, OrderService>();
            services.AddScoped<IndicatorService, IndicatorService>();
            services.AddScoped<LineService, LineService>();
            services.AddScoped<StatusService, StatusService>();
            services.AddScoped<LineChartService, LineChartService>();

            // Jobs
            services.AddScoped<MainJob, MainJob>();
            services.AddScoped<UpdateCurrenciesJob, UpdateCurrenciesJob>();
            services.AddScoped<UpdateLinesJob, UpdateLinesJob>();
            services.AddScoped<UpdateDefaultWatchersJob, UpdateDefaultWatchersJob>();
            services.AddScoped<UpdateWatchersJob, UpdateWatchersJob>();
            services.AddScoped<UpdateOrdersJob, UpdateOrdersJob>();
            services.AddScoped<SendWhatsappNotificationsJob, SendWhatsappNotificationsJob>();
            services.AddScoped<SendTelgramNotifications, SendTelgramNotifications>();
            services.AddScoped<RemoveLinesJob, RemoveLinesJob>();

            // Other
            //services.AddScoped<DateTimeProvider, DateTimeProvider>();
            //services.AddScoped<HttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICoinMarketCapClient, CoinMarketCapClient>();

            return services;
        }
    }
}
