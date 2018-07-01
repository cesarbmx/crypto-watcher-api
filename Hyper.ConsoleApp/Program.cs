using System;
using System.IO;
using AutoMapper;
using Hangfire;
using Hyper.Domain.Repositories;
using Hyper.Domain.Services;
using Hyper.Infrastructure.Configuration;
using Hyper.Infrastructure.Contexts;
using Hyper.Infrastructure.Jobs;
using Hyper.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NoobsMuc.Coinmarketcap.Client;
using Microsoft.EntityFrameworkCore;

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
                .AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Hyper")))
                .AddSingleton<ICurrencyRepository, CurrencyRepository>()
                .AddSingleton<CurrencyService, CurrencyService>()
                .AddSingleton<ICoinmarketcapClient, CoinmarketcapClient>()
                .AddSingleton<CurrencyJob, CurrencyJob>()
                .BuildServiceProvider();

            // Logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(configuration.GetSection("Logging"));

            // Hangfire
            GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("Hyper"));
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
