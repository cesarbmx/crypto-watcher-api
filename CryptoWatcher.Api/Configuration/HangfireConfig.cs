using CesarBmx.Shared.Api.Configuration;
using CesarBmx.Shared.Api.Settings;
using CryptoWatcher.Application.Jobs;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoWatcher.Api.Configuration
{
    public static class HangfireConfig
    {
        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            //services.ConfigurePinnacleHangfire();

            services.AddHangfire(x => x.UseMemoryStorage());


            // Return
            return services;
        }
        public static IApplicationBuilder ConfigureHangfire(this IApplicationBuilder app, IConfiguration configuration, IHostEnvironment env)
        {
            // Enable basic only for Staging/Production
            app.ConfigureSharedHangfire(env.IsStaging() || env.IsProduction());

            // Grab AppSettings
            var appSettings = new AppSettings();
            configuration.GetSection("AppSettings").Bind(appSettings);

            // Background jobs
            var jobsIntervalInMinutes = int.Parse(configuration["AppSettings:JobsIntervalInMinutes"]);
            RecurringJob.AddOrUpdate<MainJob>("Main", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            RecurringJob.AddOrUpdate<SendNotificationsViaWhatsappJob>("Send notifications via whatsapp", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            RecurringJob.AddOrUpdate<SendNotificationsViaTelgramJob>("Send notifications via telegram", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            RecurringJob.AddOrUpdate<RemoveObsoleteLinesJob>("Remove obsolete lines", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");

            return app;
        }
    }
}
