using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Automapper;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Service.Configuration
{
    public static class AutomapperConfig
    {
        public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CurrencyMapping).Assembly);

            return services;
        }
    }
}
