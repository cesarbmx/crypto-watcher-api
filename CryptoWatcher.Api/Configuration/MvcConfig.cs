using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using CryptoWatcher.Application.Validators;

namespace CryptoWatcher.Api.Configuration
{
    public static class MvcConfig
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.ConfigureSharedMvc(typeof(AddUserValidator), true);
            
            return services;
        }

        public static IApplicationBuilder ConfigureMvc(this IApplicationBuilder app)
        {
            app.ConfigureSharedMvc(true);

            return app;
        }
    }
}
