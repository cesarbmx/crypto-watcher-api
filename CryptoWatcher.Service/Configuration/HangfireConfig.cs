using System;
using Hangfire;
using Hangfire.MemoryStorage;
using CryptoWatcher.BackgroundJobs;
using Hangfire.AspNetCore;
using Hangfire.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LogLevel = Hangfire.Logging.LogLevel;

namespace CryptoWatcher.Service.Configuration
{
    public static class HangfireConfig
    {
        public static void ConfigureHangfire(this ServiceProvider serviceProvider, IConfiguration configuration)
        {
            var scopeFactory = (IServiceScopeFactory)serviceProvider.GetService(typeof(IServiceScopeFactory));
            GlobalConfiguration.Configuration.UseLogProvider(new HangfireLoggerProvider());
            GlobalConfiguration.Configuration.UseActivator(new AspNetCoreJobActivator(scopeFactory));

            // Hangfire
            // UseMemoryStorage
            if (bool.Parse(configuration["AppSettings:UseMemoryStorage"]))
            {
                GlobalConfiguration.Configuration.UseMemoryStorage();
            }
            else
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("CryptoWatcher"));
            }

            // Background jobs
            var jobsIntervalInMinutes = int.Parse(configuration["AppSettings:JobsIntervalInMinutes"]);

            var mainJob = serviceProvider.GetService<MainJob>();
            var sendWhatsappNotificationsJob = serviceProvider.GetService<SendWhatsappNotificationsJob>();
            var sendTelegramNotificationsJob = serviceProvider.GetService<SendWhatsappNotificationsJob>();
            var removeLinesJob = serviceProvider.GetService<RemoveOldLinesJob>();

            RecurringJob.AddOrUpdate("Main", () => mainJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));           
            RecurringJob.AddOrUpdate("Send whatsapp notifications", () => sendWhatsappNotificationsJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate("Send telegram notifications", () => sendTelegramNotificationsJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate("Remove lines", () => removeLinesJob.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
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
