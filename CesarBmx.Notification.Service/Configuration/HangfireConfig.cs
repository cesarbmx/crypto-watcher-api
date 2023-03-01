using System;
using CesarBmx.Notification.Application.Jobs;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.Notification.Service.Configuration
{
    public static class HangfireConfig
    {
        public static IServiceProvider ConfigureHangfire(this IServiceProvider serviceProvider, IConfiguration configuration)
        {
            // Hangfire
            if (bool.Parse(configuration["AppSettings:UseMemoryStorage"]))
            {
                GlobalConfiguration.Configuration.UseMemoryStorage();
            }
            else
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("Notification"));
            }

            // Background jobs
            var jobsIntervalInMinutes = int.Parse(configuration["AppSettings:JobsIntervalInMinutes"]);

            var mainJob = serviceProvider.GetService<MainJob>();
            var sendWhatsappNotificationsJob = serviceProvider.GetService<SendWhatsappNotificationsJob>();
            var sendTelegramNotificationsJob = serviceProvider.GetService<SendWhatsappNotificationsJob>();
            var removeLinesJob = serviceProvider.GetService<RemoveObsoleteLinesJob>();

            // Current
            JobStorage.Current = new MemoryStorage();

            RecurringJob.AddOrUpdate("Main", () => mainJob.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            RecurringJob.AddOrUpdate("Send whatsapp notifications", () => sendWhatsappNotificationsJob.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            RecurringJob.AddOrUpdate("Send telegram notifications", () => sendTelegramNotificationsJob.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            RecurringJob.AddOrUpdate("Remove obsolete lines", () => removeLinesJob.Run(), $"*/{jobsIntervalInMinutes} * * * *");

            return serviceProvider;
        }
    }
}
