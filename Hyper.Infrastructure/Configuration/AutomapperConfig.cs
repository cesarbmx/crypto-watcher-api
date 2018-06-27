using System;
using AutoMapper;
using Hyper.Domain.Models;

namespace Hyper.Infrastructure.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<NoobsMuc.Coinmarketcap.Client.Currency, Currency>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src.PriceUsd)))
                .ForMember(dest => dest.Volume24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.Volume24hUsd)))
                .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => Convert.ToDecimal(src.MarketCapUsd)))
                .ForMember(dest => dest.PercentageChange24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.PercentChange24h)));
        }
    }
}
