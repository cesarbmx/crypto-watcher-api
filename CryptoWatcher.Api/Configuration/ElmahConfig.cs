using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class ElmahConfig
    {
        public static IServiceCollection ConfigureElmah(this IServiceCollection services)
        {
            services.ConfigureSharedElmah();

            // Return
            return services;
        }
        public static IApplicationBuilder ConfigureElmah(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.ConfigureSharedElmah();

            return app;
        }
    }
}
