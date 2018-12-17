using System;
using Hangfire;
using Hangfire.MemoryStorage;
using CryptoWatcher.BackgroundJobs;
using Hangfire.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Service.Configuration
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

            var mainJob = serviceProvider.GetService<MainJob>();
            var sendWhatsappNotificationsJob = serviceProvider.GetService<SendWhatsappNotificationsJob>();
            var sendTelegramNotificationsJob = serviceProvider.GetService<SendWhatsappNotificationsJob>();
            var removeLinesJob = serviceProvider.GetService<RemoveLinesJob>();

            RecurringJob.AddOrUpdate("Main", () => mainJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));           
            RecurringJob.AddOrUpdate("Send whatsapp notifications", () => sendWhatsappNotificationsJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate("Send telegram notifications", () => sendTelegramNotificationsJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate("Remove lines", () => removeLinesJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));

            // Run them on startup
            BackgroundJob.Enqueue(() => mainJob.Run());
            BackgroundJob.Enqueue(() => sendWhatsappNotificationsJob.Run());
            BackgroundJob.Enqueue(() => sendTelegramNotificationsJob.Run());
            BackgroundJob.Enqueue(() => removeLinesJob.Run());
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
