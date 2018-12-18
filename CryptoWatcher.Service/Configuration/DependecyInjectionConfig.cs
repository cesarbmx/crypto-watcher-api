using CoinMarketCap;
using CoinMarketCap.Core;
using CryptoWatcher.BackgroundJobs;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Service.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Contexts (UOW)
            //services.AddDbContext<MainDbContext>(options => options
            //    .UseSqlServer(configuration.GetConnectionString("CryptoWatcher"))
            //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddDbContext<MainDbContext>(options => options
                .UseInMemoryDatabase("CryptoWatcher")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


            // Repositories
            services.AddTransient<IRepository<Log>, Repository<Log>>();
            services.AddTransient<IRepository<Currency>, Repository<Currency>>();
            services.AddTransient<IRepository<Watcher>, Repository<Watcher>>();
            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IRepository<Notification>, Repository<Notification>>();
            services.AddTransient<IRepository<Order>, Repository<Order>>();
            services.AddTransient<IRepository<Indicator>, Repository<Indicator>>();
            services.AddTransient<IRepository<Line>, Repository<Line>>();

            // Jobs
            services.AddTransient<MainJob, MainJob>();
            services.AddTransient<UpdateCurrenciesJob, UpdateCurrenciesJob>();
            services.AddTransient<UpdateLinesJob, UpdateLinesJob>();
            services.AddTransient<UpdateDefaultWatchersJob, UpdateDefaultWatchersJob>();
            services.AddTransient<UpdateWatchersJob, UpdateWatchersJob>();
            services.AddTransient<UpdateOrdersJob, UpdateOrdersJob>();
            services.AddTransient<SendWhatsappNotificationsJob, SendWhatsappNotificationsJob>();
            services.AddTransient<SendTelgramNotifications, SendTelgramNotifications>();
            services.AddScoped<RemoveLinesJob, RemoveLinesJob>();
            services.AddScoped<RemoveLinesJob, RemoveLinesJob>();

            // Other
            services.AddTransient<ICoinMarketCapClient, CoinMarketCapClient>();
            services.AddSingleton(configuration);

            return services;
        }
    }
}
