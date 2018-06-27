using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Hyper.App.Services;
using Hyper.Domain.Repositories;
using Hyper.Infrastructure.Loggers;
using Hyper.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using NoobsMuc.Coinmarketcap.Client;
using Owin;

namespace Hyper.ConsoleApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
#if DEBUG
            app.UseErrorPage();
#endif
            app.UseWelcomePage("/");

            GlobalConfiguration.Configuration.UseMemoryStorage();
        }
        public void Configure(IApplicationBuilder app, IRecurringJobManager recurringJobManager)
        {
            // Initialize Hangfire
            GlobalConfiguration.Configuration.UseMemoryStorage();

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // Register jobs
            var currencyService = Program.GetCurrencyService();
            RecurringJob.AddOrUpdate("HelloWorld", () => Console.WriteLine("Hello, world!"), Cron.Minutely);
            RecurringJob.AddOrUpdate("ImportAllCurrenciesFromCoinMarketCap", () => currencyService.ImportAllCurrenciesFromCoinMarketCap(), Cron.Minutely);

        }
    }
}
