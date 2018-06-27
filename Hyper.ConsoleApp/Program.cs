using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Hyper.App.Services;
using Hyper.Domain.Repositories;
using Hyper.Infrastructure.Loggers;
using Hyper.Infrastructure.Repositories;
using NoobsMuc.Coinmarketcap.Client;


namespace Hyper.ConsoleApp
{
    public class Program
    {
        public static ICurrencyRepository GetICurrencyRepository()
        {
            var currencyRepository = new CurrencyRepository();
            return currencyRepository;
        }
        public static CurrencyService GetCurrencyService()
        {
            var logger = new ConsoleLogger();
            var coinmarketcapClient = new CoinmarketcapClient();
            var currencyRepository = GetICurrencyRepository();
            var importCurrenciesJob = new CurrencyService(logger, coinmarketcapClient, currencyRepository);
            return importCurrenciesJob;
        }


        public static void Main(string[] args)
        {
            try
            {
                // Use in memory storage
                GlobalConfiguration.Configuration.UseMemoryStorage();

                // Register jobs
                var currencyService = GetCurrencyService();
                //RecurringJob.AddOrUpdate("HelloWorld", () => Console.WriteLine("Hello, world!"), Cron.Minutely);
                RecurringJob.AddOrUpdate("ImportAllCurrenciesFromCoinMarketCap", () => currencyService.ImportAllCurrenciesFromCoinMarketCap(), Cron.Minutely);

                using (new BackgroundJobServer())
                {
                    Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
