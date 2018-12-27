using CryptoWatcher.Application.Lines.Requests;
using Hangfire;
using Hangfire.MemoryStorage;
using CryptoWatcher.BackgroundJobs;
using CryptoWatcher.Shared.Hangfire;
using Hangfire.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CryptoWatcher.Api.Configuration
{
    public static class HangfireConfig
    {
        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            // UseMemoryStorage
            if (bool.Parse(configuration["AppSettings:UseMemoryStorage"]))
            {
                services.AddHangfire(x => x.UseStorage(GlobalConfiguration.Configuration.UseMemoryStorage()));
            }
            else
            {
                services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("CryptoWatcher")));
            }

            // Return
            return services;
        }
        public static IApplicationBuilder ConfigureHangfire(this IApplicationBuilder app, IConfiguration configuration, IMediator mediator)
        {
            // Configure
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // HangfireMediator https://codeopinion.com/background-commands-mediatr-hangfire/
            GlobalConfiguration.Configuration.UseActivator(new MediatRJobActivator(mediator));
            JobHelper.SetSerializerSettings(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            });

            // Background jobs
            var jobsIntervalInMinutes = int.Parse(configuration["AppSettings:JobsIntervalInMinutes"]);
            RecurringJob.AddOrUpdate<MainJob>("Main", x => x.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate<SendWhatsappNotificationsJob>("Send whatsapp notifications", x => x.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate<SendTelgramNotifications>("Send telegram notifications", x => x.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate<HangfireMediator>("Remove lines", x => x.Send(new RemoveOldLinesRequest()), Cron.MinuteInterval(jobsIntervalInMinutes));

            return app;
        }
    }
}
