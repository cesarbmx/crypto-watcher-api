using CesarBmx.CryptoWatcher.Application.Validators;
using CesarBmx.Shared.Api.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api2.Configuration
{
    public static class FluentValidationConfig
    {
        public static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
        {
            services.ConfigureFluentValidation(typeof(AddUserValidator));

            // Return
            return services;
        }
    }
}
