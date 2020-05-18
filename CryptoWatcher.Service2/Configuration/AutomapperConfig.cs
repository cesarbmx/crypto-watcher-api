using System;
using AutoMapper;
using CoinMarketCap.Entities;
using Microsoft.Extensions.DependencyInjection;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Service.Configuration
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
