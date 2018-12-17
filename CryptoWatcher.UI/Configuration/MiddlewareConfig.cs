using Microsoft.AspNetCore.Builder;
using CryptoWatcher.UI.Middlewares;

namespace CryptoWatcher.UI.Configuration
{
    public static class MiddlewareConfig
    {
        public static IApplicationBuilder ConfigureMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            return app;
        }
    }
}
