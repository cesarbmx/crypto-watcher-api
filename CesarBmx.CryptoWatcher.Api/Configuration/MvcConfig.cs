using CesarBmx.CryptoWatcher.Application.Validators;
using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class MvcConfig
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.ConfigureSharedMvc(typeof(AddIndicatorValidator), true);
            
            return services;
        }

        public static IApplicationBuilder ConfigureMvc(this IApplicationBuilder app)
        {
            app.ConfigureSharedMvc(true);

            return app;
        }
    }
}
