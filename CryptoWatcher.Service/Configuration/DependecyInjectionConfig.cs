using CoinMarketCap;
using CoinMarketCap.Core;
using CryptoWatcher.BackgroundJobs;
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

            // Jobs
            services.AddTransient<MainJob, MainJob>();
            services.AddTransient<UpdateCurrenciesJob, UpdateCurrenciesJob>();
            services.AddTransient<UpdateLinesJob, UpdateLinesJob>();
            services.AddTransient<UpdateDefaultWatchersJob, UpdateDefaultWatchersJob>();
            services.AddTransient<UpdateWatchersJob, UpdateWatchersJob>();
            services.AddTransient<UpdateOrdersJob, UpdateOrdersJob>();
            services.AddTransient<SendWhatsappNotificationsJob, SendWhatsappNotificationsJob>();
            services.AddTransient<SendTelgramNotifications, SendTelgramNotifications>();
            services.AddTransient<RemoveLinesJob, RemoveLinesJob>();

            // Other
            services.AddTransient<ICoinMarketCapClient, CoinMarketCapClient>();
            services.AddSingleton(configuration);

            return services;
        }
    }
}
