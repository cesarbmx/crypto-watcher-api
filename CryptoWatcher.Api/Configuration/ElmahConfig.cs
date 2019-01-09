using ElmahCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class ElmahConfig
    {
        public static IServiceCollection ConfigureElmah(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmah(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("CryptoWatcher");
            });

            // Return
            return services;
        }
        public static IApplicationBuilder ConfigureElmah(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseElmah();

            return app;
        }
    }
}
