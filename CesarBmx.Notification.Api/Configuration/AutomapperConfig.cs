using AutoMapper;
using CesarBmx.Notification.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.Notification.Api.Configuration
{
    public static class AutomapperConfig
    {
        public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CurrencyMapper).Assembly);

            return services;
        }
    }
}
