using Microsoft.AspNetCore.Builder;
using CryptoWatcher.Web.Middlewares;

namespace CryptoWatcher.Web.Configuration
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
