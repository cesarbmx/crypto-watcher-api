using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using CoinMarketCap;
using CoinMarketCap.Core;
using Hangfire;
using Hyper.Domain.Services;
using Hyper.Infrastructure.Configuration;
using Hyper.Infrastructure.Contexts;
using Hyper.Infrastructure.Jobs;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Hangfire.MemoryStorage;
using Hyper.Domain.Repositories;
using Hyper.Infrastructure.Repositories;

namespace Hyper.ConsoleApp
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
                cfg.AddProfile(new AutomapperConfig());
            });

            // DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IMapper>(factory => new Mapper(automapperConfig))
                //.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Hyper")))
                .AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("Hyper"))
                .AddSingleton<CacheService, CacheService>()
                .AddSingleton<ICacheRepository, CacheRepository>()
                .AddSingleton<CurrencyService, CurrencyService>()
                .AddSingleton<LogService, LogService>()
                .AddSingleton<ILogRepository, LogRepository>()
                .AddSingleton<ICoinMarketCapClient, CoinMarketCapClient>()
                .AddSingleton<CurrencyJob, CurrencyJob>()
                .BuildServiceProvider();

            // Logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(configuration.GetSection("Logging"));

            // Log4Net
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogInformation("LogInformation");

            // Hangfire
            //GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("Hyper"));
            GlobalConfiguration.Configuration.UseMemoryStorage();
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));

            // Register jobs
            var currencyJob = serviceProvider.GetService<CurrencyJob>();
            RecurringJob.AddOrUpdate("Import currencies", () => currencyJob.Import(), Cron.Minutely);

            using (new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
                Console.ReadLine();
            }
        }
    }
}
