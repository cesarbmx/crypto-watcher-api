using CesarBmx.CryptoWatcher.Application.Validators;
using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api2.Configuration
{
    public static class MvcConfig
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSharedMvc(configuration, true);
            
            return services;
        }

        public static IApplicationBuilder ConfigureMvc(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.ConfigureSharedMvc(configuration, true);

            return app;
        }
    }
}
