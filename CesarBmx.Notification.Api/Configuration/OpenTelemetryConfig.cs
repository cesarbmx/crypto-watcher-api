using System.Reflection;
using CesarBmx.Shared.Api.Configuration;

namespace CesarBmx.Notification.Api.Configuration
{
    public static class OpenTelemetryConfig
    {
        public static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSharedOpenTelemetry(configuration, Assembly.GetExecutingAssembly());

            return services;
        }
        public static IApplicationBuilder ConfigureOpenTelemetry(this IApplicationBuilder app)
        {
            app.ConfigureSharedOpenTelemetry();

            return app;
        }
    }
}
