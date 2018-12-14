using CoinMarketCap;
using CoinMarketCap.Core;
using CryptoWatcher.BackgroundJobs;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Domain.Services;
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
            //services.AddScoped<IPinnacleTokenService<CryptoWatcherPermission>, PinnacleTokenService<CryptoWatcherPermission>>();

            //Contexts (UOW)
            //services.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CryptoWatcher")));
            services.AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("CryptoWatcher"), ServiceLifetime.Transient);

            // Services
            services.AddTransient<CacheService, CacheService>();

            // Repositories
            services.AddTransient<IRepository<Cache>, Repository<Cache>>();
            services.AddTransient<IRepository<Log>, Repository<Log>>();
            services.AddTransient<IRepository<Watcher>, Repository<Watcher>>();
            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IRepository<Notification>, Repository<Notification>>();
            services.AddTransient<IRepository<Order>, Repository<Order>>();
            services.AddTransient<IRepository<Indicator>, Repository<Indicator>>();

            // Jobs
            services.AddTransient<MainJob, MainJob>();
            services.AddTransient<UpdateCurrenciesJob, UpdateCurrenciesJob>();
            services.AddTransient<UpdateLinesJob, UpdateLinesJob>();
            services.AddTransient<UpdateDefaultWatchersJob, UpdateDefaultWatchersJob>();
            services.AddTransient<UpdateWatchersJob, UpdateWatchersJob>();
            services.AddTransient<UpdateOrdersJob, UpdateOrdersJob>();
            services.AddTransient<SendWhatsappNotificationsJob, SendWhatsappNotificationsJob>();
            services.AddTransient<SendTelgramNotifications, SendTelgramNotifications>();

            // Other
            services.AddTransient<ICoinMarketCapClient, CoinMarketCapClient>();
            services.AddSingleton(configuration);

            return services;
        }
    }
}
