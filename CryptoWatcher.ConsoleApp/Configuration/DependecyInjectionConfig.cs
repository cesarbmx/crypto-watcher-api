using CoinMarketCap;
using CoinMarketCap.Core;
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
            services.AddScoped<CacheService, CacheService>();
            services.AddScoped<CurrencyService, CurrencyService>();
            services.AddScoped<StatusService, StatusService>();
            services.AddScoped<ErrorMessagesService, ErrorMessagesService>();
            services.AddScoped<LogService, LogService>();
            services.AddScoped<WatcherService, WatcherService>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<NotificationService, NotificationService>();
            services.AddScoped<OrderService, OrderService>();

            // Repositories
            services.AddScoped<ICacheRepository, CacheRepository>(); // TODO: (Cesar) app settings switch for audit
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IWatcherRepository, WatcherRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Other
            services.AddScoped<ICoinMarketCapClient, CoinMarketCapClient>();

            return services;
        }
    }
}
