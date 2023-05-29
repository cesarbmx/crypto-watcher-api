using CesarBmx.CryptoWatcher.Application.Validators;
using CesarBmx.Shared.Api.Configuration;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class FluentValidationConfig
    {
        public static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
        {
            services.ConfigureSharedFluentValidation(typeof(AddUserValidator));

            // Return
            return services;
        }
    }
}
