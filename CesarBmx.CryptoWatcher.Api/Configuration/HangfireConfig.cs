using CesarBmx.Shared.Api.Configuration;
using CesarBmx.CryptoWatcher.Application.Jobs;
using CesarBmx.CryptoWatcher.Application.Settings;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class HangfireConfig
    {
        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSharedHangfire();

            // Grab AppSettings
            var appSettings = new AppSettings();
            configuration.GetSection("AppSettings").Bind(appSettings);

            if (appSettings.UseMemoryStorage)
            {
                services.AddHangfire(x => x.UseMemoryStorage());
            }
            else
            {
                services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("CryptoWatcher"), new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true
                }));
            }


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
            //RecurringJob.AddOrUpdate<SendWhatsappNotificationsJob>("Send whatsapp notifications", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            RecurringJob.AddOrUpdate<SendTelgramNotificationsJob>("Send telegram notifications", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            //RecurringJob.AddOrUpdate<RemoveObsoleteLinesJob>("Remove obsolete lines", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");

            return app;
        }
    }
}
