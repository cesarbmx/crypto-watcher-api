﻿using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api.Configuration
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
