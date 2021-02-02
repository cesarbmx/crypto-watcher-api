using System;
using AutoMapper;
using CesarBmx.Shared.Common.Extensions;
using CoinpaprikaAPI.Entity;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class CurrencyMapping : Profile
    {
        public CurrencyMapping()
        {
            CreateMap<Currency, Responses.Currency>();
            CreateMap<TickerWithQuotesInfo, Currency>()
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Symbol.ToLower()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src.Quotes["USD"].Price)))
                .ForMember(dest => dest.Volume24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.Quotes["USD"].Volume24H)))
                .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => Convert.ToDecimal(src.Quotes["USD"].MarketCap)))
                .ForMember(dest => dest.PercentageChange24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.Quotes["USD"].PercentChange24H)))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTime.UtcNow.StripSeconds()));
        }
    }
}
