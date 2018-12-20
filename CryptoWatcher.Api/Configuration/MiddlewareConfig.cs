using Microsoft.AspNetCore.Builder;
using CryptoWatcher.Shared.Middlewares;

namespace CryptoWatcher.Api.Configuration
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
