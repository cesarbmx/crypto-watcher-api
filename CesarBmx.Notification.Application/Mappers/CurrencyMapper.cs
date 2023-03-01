using System;
using AutoMapper;
using CesarBmx.Shared.Common.Extensions;
using CoinpaprikaAPI.Entity;
using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Application.Mappers
{
    public class CurrencyMapper : Profile
    {
        public CurrencyMapper()
        {
            CreateMap<Currency, Responses.Currency>();
            CreateMap<TickerWithQuotesInfo, Currency>()
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Symbol))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Math.Truncate(src.Quotes["USD"].Price)))
                .ForMember(dest => dest.Volume24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.Quotes["USD"].Volume24H)))
                .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => Convert.ToDecimal(src.Quotes["USD"].MarketCap)))
                .ForMember(dest => dest.PercentageChange24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.Quotes["USD"].PercentChange24H)))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTime.UtcNow.StripSeconds()));
        }
    }
}
