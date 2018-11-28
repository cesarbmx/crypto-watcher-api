using CoinMarketCap;
using CoinMarketCap.Core;
using CryptoWatcher.BackgroundJobs;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.ConsoleApp.Configuration
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
            services.AddSingleton<CurrencyService, CurrencyService>();
            services.AddSingleton<StatusService, StatusService>();
            services.AddSingleton<ErrorMessagesService, ErrorMessagesService>();
            services.AddSingleton<LogService, LogService>();
            services.AddSingleton<WatcherService, WatcherService>();
            services.AddSingleton<UserService, UserService>();
            services.AddSingleton<NotificationService, NotificationService>();
            services.AddSingleton<OrderService, OrderService>();

            // Repositories
            services.AddSingleton<ICacheRepository, CacheRepository>(); // TODO: (Cesar) app settings switch for audit
            services.AddSingleton<ILogRepository, LogRepository>();
            services.AddSingleton<IWatcherRepository, WatcherRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<INotificationRepository, NotificationRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

            // Jobs
            services.AddSingleton<ImportCurrenciesJob, ImportCurrenciesJob>();
            services.AddSingleton<MonitorWatchersJob, MonitorWatchersJob>();
            services.AddSingleton<SendWhatsappNotificationsJob, SendWhatsappNotificationsJob>();

            // Other
            services.AddSingleton<ICoinMarketCapClient, CoinMarketCapClient>();

            return services;
        }
    }
}
