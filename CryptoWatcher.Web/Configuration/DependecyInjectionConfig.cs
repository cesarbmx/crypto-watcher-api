using CoinMarketCap;
using CoinMarketCap.Core;
using CryptoWatcher.BackgroundJobs;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Web.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            //Contexts (UOW)
            //services.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CryptoWatcher")));
            services.AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("CryptoWatcher"));

            // Services
            services.AddScoped<CacheService, CacheService>();

            // Repositories
            services.AddScoped<Repository<Cache>, Repository<Cache>>();
            services.AddScoped<Repository<Log>, Repository<Log>>();
            services.AddScoped<Repository<Watcher>, Repository<Watcher>>();
            services.AddScoped<Repository<User>, Repository<User>>();
            services.AddScoped<Repository<Notification>, Repository<Notification>>();
            services.AddScoped<Repository<Order>, Repository<Order>>();
            services.AddScoped<Repository<Indicator>, Repository<Indicator>>();

            services.AddScoped<IRepository<Cache>, LoggerRepository<Cache>>();
            services.AddScoped<IRepository<Log>, Repository<Log>>();
            services.AddScoped<IRepository<Watcher>, LoggerRepository<Watcher>>();
            services.AddScoped<IRepository<User>, LoggerRepository<User>>();
            services.AddScoped<IRepository<Notification>, LoggerRepository<Notification>>();
            services.AddScoped<IRepository<Order>, LoggerRepository<Order>>();
            services.AddScoped<IRepository<Indicator>, LoggerRepository<Indicator>>();

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
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<HttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICoinMarketCapClient, CoinMarketCapClient>();

            return services;
        }
    }
}
