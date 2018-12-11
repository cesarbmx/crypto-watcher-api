using Hangfire;
using Hangfire.MemoryStorage;
using CryptoWatcher.BackgroundJobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class HangfireConfig
    {
        public static IServiceCollection ConfigureHangfire(this IServiceCollection services)
        {
            //services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("CryptoWatcher")));
            var inMemoryStorage = GlobalConfiguration.Configuration.UseMemoryStorage();
            services.AddHangfire(x => x.UseStorage(inMemoryStorage));

            return services;
        }
        public static IApplicationBuilder ConfigureHangfire(this IApplicationBuilder app, IConfiguration configuration)
        {
            // Configure
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // Background jobs
            var jobsIntervalInMinutes = int.Parse(configuration["JobsIntervalInMinutes"]);
            RecurringJob.AddOrUpdate<UpdateCacheJob>("Update cache", x => x.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate<UpdateWatchersJob>("Update watchers", x => x.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate<UpdateOrdersJob>("Update orders", x => x.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate<SendWhatsappNotificationsJob>("Send whatsapp notifications", x => x.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate<SendTelgramNotifications>("Send telegram notifications", x => x.Run(), Cron.MinuteInterval(jobsIntervalInMinutes));

            // Run them on startup
            BackgroundJob.Enqueue<UpdateCacheJob>(x => x.Run());
            BackgroundJob.Enqueue<UpdateWatchersJob>(x => x.Run());
            BackgroundJob.Enqueue<UpdateOrdersJob>(x => x.Run());
            BackgroundJob.Enqueue<SendWhatsappNotificationsJob>(x => x.Run());
            BackgroundJob.Enqueue<SendTelgramNotifications>(x => x.Run());

            return app;
        }
    }
}
