using CesarBmx.Shared.Api.Configuration;
using CesarBmx.CryptoWatcher.Application.Jobs;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CesarBmx.Shared.Application.Settings;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class HangfireConfig
    {
        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSharedHangfire();

            // Grab AppSettings
            var appSettings = configuration.GetSection<Application.Settings.AppSettings>();

            // Grab EnvironmentSettings
            var environmentSettings = configuration.GetSection<EnvironmentSettings>();

            if (appSettings.UseMemoryStorage)
            {
                services.AddHangfire(x => x.UseMemoryStorage());
            }
            else
            {
                services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("CryptoWatcher"), new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = environmentSettings.Name == "Development",
                    SchemaName = "Hangfire"
                }));
            }


            // Return
            return services;
        }
        public static IApplicationBuilder ConfigureHangfire(this IApplicationBuilder app, IConfiguration configuration)
        {
            // Grab EnvironmentSettings
            var environmentSettings = configuration.GetSection<EnvironmentSettings>();

            // Enable basic auth only for Staging/Production
            app.ConfigureSharedHangfire(environmentSettings.Name == "Staging" || environmentSettings.Name == "Production");

            // Grab AppSettings
            var appSettings = configuration.GetSection<Application.Settings.AppSettings>();

            // Background jobs
            var jobsIntervalInMinutes = appSettings.JobsIntervalInMinutes;
            RecurringJob.AddOrUpdate<MainJob>("Main", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            //RecurringJob.AddOrUpdate<SendWhatsappNotificationsJob>("Send whatsapp notifications", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            //RecurringJob.AddOrUpdate<SendTelgramNotificationsJob>("Send telegram notifications", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");
            //RecurringJob.AddOrUpdate<RemoveObsoleteLinesJob>("Remove obsolete lines", x => x.Run(), $"*/{jobsIntervalInMinutes} * * * *");

            return app;
        }
    }
}
