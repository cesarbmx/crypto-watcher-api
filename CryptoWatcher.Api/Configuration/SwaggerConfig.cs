using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CryptoWatcher.Api.ResponseExamples;

namespace CryptoWatcher.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.ConfigureSharedSwagger("CryptoWatcher API", typeof(UserResponseExample));

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IConfiguration config)
        {
            app.ConfigureSharedSwagger();

            return app;
        }
    }
}
