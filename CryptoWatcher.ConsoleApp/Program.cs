using System;
using System.IO;
using AutoMapper;
using CoinMarketCap;
using CoinMarketCap.Core;
using CoinMarketCap.Entities;
using CryptoWatcher.BackgroundJobs;
using CryptoWatcher.Domain.Models;
using Hangfire;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Hangfire.MemoryStorage;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Persistence.Repositories;
using Hangfire.Logging;

namespace CryptoWatcher.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            // Automapper
            var automapperConfig = new MapperConfiguration(cfg =>
            {
                // Others
                cfg.CreateMap<TickerEntity, Currency>()
                    .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.CurrencyPrice, opt => opt.MapFrom(src => Convert.ToDecimal(src.PriceUsd)))
                    .ForMember(dest => dest.CurrencyVolume24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.Volume24hUsd)))
                    .ForMember(dest => dest.CurrencyMarketCap, opt => opt.MapFrom(src => Convert.ToDecimal(src.MarketCapUsd)))
                    .ForMember(dest => dest.CurrencyPercentageChange24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.PercentChange24h)));
            });

            // DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(cfg => cfg.AddLog4Net("log4net.config").AddConfiguration(configuration.GetSection("Logging")))
                .AddSingleton<IMapper>(factory => new Mapper(automapperConfig))
                //.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CryptoWatcher")))
                .AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("CryptoWatcher"))
                .AddSingleton<CacheService, CacheService>()
                .AddSingleton<ICacheRepository, CacheRepository>()
                .AddSingleton<CurrencyService, CurrencyService>()
                .AddSingleton<LogService, LogService>()
                .AddSingleton<ILogRepository, LogRepository>()
                .AddSingleton<ICoinMarketCapClient, CoinMarketCapClient>()
                .AddSingleton<ImportCurrenciesJob, ImportCurrenciesJob>()
                .BuildServiceProvider();

            // Logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddLog4Net("log4net.config");


            // Hangfire
            LogProvider.SetCurrentLogProvider(new HangfireLoggerProvider());
            //GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("CryptoWatcher"));
            GlobalConfiguration.Configuration.UseMemoryStorage();
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));

            // Register jobs
            var currencyJob = serviceProvider.GetService<ImportCurrenciesJob>();
            RecurringJob.AddOrUpdate("Import currencies", () => currencyJob.Run(), Cron.Minutely);

            using (new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
                Console.ReadLine();
            }
        }
    }
}
