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
            services.AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("CryptoWatcher"));

            // Services
            services.AddSingleton<CacheService, CacheService>();

            // Repositories
            services.AddScoped<IRepository<Cache>, Repository<Cache>>();
            services.AddScoped<IRepository<Log>, Repository<Log>>();
            services.AddScoped<IRepository<Watcher>, Repository<Watcher>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Notification>, Repository<Notification>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();

            // Jobs
            services.AddScoped<MainJob, MainJob>();
            services.AddScoped<UpdateCurrenciesJob, UpdateCurrenciesJob>();
            services.AddScoped<UpdateLinesJob, UpdateLinesJob>();
            services.AddScoped<UpdateDefaultWatchersJob, UpdateDefaultWatchersJob>();
            services.AddScoped<UpdateWatchersJob, UpdateWatchersJob>();
            services.AddScoped<UpdateOrdersJob, UpdateOrdersJob>();
            services.AddScoped<SendWhatsappNotificationsJob, SendWhatsappNotificationsJob>();
            services.AddScoped<SendTelgramNotifications, SendTelgramNotifications>();

            // Other
            services.AddSingleton<ICoinMarketCapClient, CoinMarketCapClient>();

            return services;
        }
    }
}
