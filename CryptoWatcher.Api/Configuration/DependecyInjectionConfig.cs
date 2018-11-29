using System.Reflection;
using System.Security.Principal;
using CoinMarketCap;
using CoinMarketCap.Core;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
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
            services.AddScoped<StatusService, StatusService>();
            services.AddScoped<ErrorMessagesService, ErrorMessagesService>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<NotificationService, NotificationService>();
            services.AddScoped<OrderService, OrderService>();

            // Repositories
            services.AddScoped<IRepository<Cache>, Repository<Cache>>();
            services.AddScoped<IRepository<Log>, Repository<Log>>();
            services.AddScoped<IRepository<Watcher>, Repository<Watcher>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Notification>, Repository<Notification>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();

            // Other
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<HttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICoinMarketCapClient, CoinMarketCapClient>();
            services.AddScoped(factory => Assembly.GetExecutingAssembly());
            services.AddScoped<IPrincipal>( x => x.GetService<IHttpContextAccessor>().HttpContext.User);

            return services;
        }
    }
}
