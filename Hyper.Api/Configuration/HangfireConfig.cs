using Hangfire;
using Hangfire.MemoryStorage;
using CryptoWatcher.Api.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class HangfireConfig
    {
        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
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
            RecurringJob.AddOrUpdate<ImportCurrenciesJob>("Import currencies", x => x.Execute(), Cron.MinuteInterval(jobsIntervalInMinutes));
            RecurringJob.AddOrUpdate<SetHypesJob>("Find hypes", x => x.Execute(), Cron.MinuteInterval(jobsIntervalInMinutes));

            return app;
        }
    }
}
