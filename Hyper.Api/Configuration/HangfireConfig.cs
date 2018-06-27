using Hangfire;
using Hyper.Infrastructure.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hyper.Api.Configuration
{
    public static class HangfireConfig
    {
        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("OwnedDB")));

            return services;
        }
        public static IApplicationBuilder ConfigureHangfire(this IApplicationBuilder app, IConfiguration configuration)
        {
            // Queue name
            var queue = "Hyper";

            // Configure
            app.UseHangfireDashboard();
            var backgroundJobServerOptions = new BackgroundJobServerOptions
            {
                Queues = new[] { queue }
            };
            app.UseHangfireServer(backgroundJobServerOptions);

            // Background jobs
            var jobsIntervalInMinutes = int.Parse(configuration["JobsIntervalInMinutes"]);
            RecurringJob.AddOrUpdate<ImportCurrenciesJob>("Hyper - Import currencies", x => x.Run(), Cron.MinuteInterval(jobsIntervalInMinutes), queue: queue);          

            return app;
        }
    }
}
