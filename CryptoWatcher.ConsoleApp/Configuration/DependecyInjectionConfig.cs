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
            // DI
            services
                //.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CryptoWatcher")))
                .AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("CryptoWatcher"))
                .AddSingleton<CacheService, CacheService>()
                .AddSingleton<ICacheRepository, CacheRepository>()
                .AddSingleton<CurrencyService, CurrencyService>()
                .AddSingleton<LogService, LogService>()
                .AddSingleton<ILogRepository, LogRepository>()
                .AddSingleton<ICoinMarketCapClient, CoinMarketCapClient>()
                .AddSingleton<ImportCurrenciesJob, ImportCurrenciesJob>();

            return services;
        }
    }
}
