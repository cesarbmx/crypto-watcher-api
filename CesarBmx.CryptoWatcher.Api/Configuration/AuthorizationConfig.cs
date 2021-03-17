using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            //services.UseSharedAuthorization(typeof(Permission));
            
            return services;
        }
    }
}
