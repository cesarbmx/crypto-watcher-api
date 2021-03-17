using CesarBmx.Shared.Api.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Shared
            //services.UseSharedAuthentication(configuration);
            services.UseSharedFakeAuthentication(configuration);

            return services;
        }
    }
}