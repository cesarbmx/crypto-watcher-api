using CryptoWatcher.Web.Middlewares;
using Microsoft.AspNetCore.Builder;

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
