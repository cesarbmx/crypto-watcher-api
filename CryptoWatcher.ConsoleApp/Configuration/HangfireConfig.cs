using System;
using Hangfire;
using Hangfire.MemoryStorage;
using CryptoWatcher.BackgroundJobs;
using Hangfire.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.ConsoleApp.Configuration
{
    public static class HangfireConfig
    {
        public static void ConfigureHangfire(this ServiceProvider serviceProvider, IConfiguration configuration)
        {
            // Hangfire
            //GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("CryptoWatcher"));
            GlobalConfiguration.Configuration.UseMemoryStorage();
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));
            GlobalConfiguration.Configuration.UseLogProvider(new HangfireLoggerProvider());

            // Background jobs
            var jobsIntervalInMinutes = int.Parse(configuration["JobsIntervalInMinutes"]);

            var importCurrenciesJob = serviceProvider.GetService<ImportCurrenciesJob>();
            var monitorWatchersJob = serviceProvider.GetService<MonitorWatchersJob>();
            //var sendWhatsappNotificationsJob = serviceProvider.GetService<SendWhatsappNotificationsJob>();

            RecurringJob.AddOrUpdate("Import currencies", () => importCurrenciesJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate("Monitor watchers", () => monitorWatchersJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            //RecurringJob.AddOrUpdate("Send whatsapp notifications", () => sendWhatsappNotificationsJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));

            // Run them on startup
            BackgroundJob.Enqueue(() => importCurrenciesJob.Run());
            BackgroundJob.Enqueue(() => monitorWatchersJob.Run());
            //BackgroundJob.Enqueue(() => sendWhatsappNotificationsJob.Run());
        }
      
    }
    public class HangfireActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public HangfireActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
    public class HangfireLoggerProvider : ILogProvider
    {
        public ILog GetLogger(string name)
        {
            return new NoLogger();
        }

        public class NoLogger : ILog
        { 
            public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception)
            {
                return false;
            }
        }
    }
}
