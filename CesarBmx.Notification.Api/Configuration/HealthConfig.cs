using CesarBmx.Shared.Api.Configuration;

namespace CesarBmx.Notification.Api.Configuration
{
    public static class HealthConfig
    {
        public static IServiceCollection ConfigureHealth(this IServiceCollection services, IConfiguration configuration)
        {
            // Shared
            services.ConfigureSharedHealth(configuration);

            // Return
            return services;
        }
        public static IApplicationBuilder ConfigureHealth(this IApplicationBuilder app)
        {
            app.ConfigureSharedHealth();

            // Return
            return app;
        }
    }
}
