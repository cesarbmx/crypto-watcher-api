using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;

namespace CesarBmx.Notification.Api.Configuration
{
    public static class MiddlewareConfig
    {
        public static IApplicationBuilder ConfigureErrorHandling(this IApplicationBuilder app)
        {
            app.ConfigureSharedMiddleware();

            return app;
        }
    }
}
