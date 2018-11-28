using System;
using AutoMapper;
using CoinMarketCap.Entities;
using Microsoft.Extensions.DependencyInjection;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.ConsoleApp.Configuration
{
    public static class AutomapperConfig
    {
        public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            // Automapper
            var automapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TickerEntity, Currency>()
                    .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.CurrencyPrice, opt => opt.MapFrom(src => Convert.ToDecimal(src.PriceUsd)))
                    .ForMember(dest => dest.CurrencyVolume24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.Volume24hUsd)))
                    .ForMember(dest => dest.CurrencyMarketCap, opt => opt.MapFrom(src => Convert.ToDecimal(src.MarketCapUsd)))
                    .ForMember(dest => dest.CurrencyPercentageChange24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.PercentChange24h)));
            });
            services.AddSingleton<IMapper>(factory => new Mapper(automapperConfig));

            // Return
            return services;
        }
    }
}
