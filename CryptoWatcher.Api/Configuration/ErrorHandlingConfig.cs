using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;

namespace CryptoWatcher.Api.Configuration
{
    public static class ErrorHandlingConfig
    {
        public static IApplicationBuilder ConfigureErrorHandling(this IApplicationBuilder app)
        {
            app.ConfigureSharedErrorHandling();

            return app;
        }
    }
}
