using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Hyper.Api.Responses;
using Hyper.Domain.Models;
using Hyper.Shared.Extensions;
using Version = Hyper.Domain.Models.Version;

namespace Hyper.Api.Configuration
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            // Responses
            CreateMap<Version, VersionResponse>();
            CreateMap<Health, HealthResponse>();
            CreateMap<Version, VersionResponse>()
                .ForMember(dest => dest.BuildDateTime, opt => opt.MapFrom(src => src.LastBuild.ToString("yyyy/MM/dd HH:mm")))
                .ForMember(dest => dest.LastBuildOccurred, opt => opt.MapFrom(src => src.LastBuild.DaysHoursMinutesAndSecondsSinceDate()));
            CreateMap<Health, HealthResponse>();

            // Models
            CreateMap<NoobsMuc.Coinmarketcap.Client.Currency, Currency>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src.PriceUsd)))
                .ForMember(dest => dest.Volume24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.Volume24hUsd)))
                .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => Convert.ToDecimal(src.MarketCapUsd)))
                .ForMember(dest => dest.PercentageChange24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.PercentChange24h)));
        }
    }

    public static class AutomapperConfig
    {
        public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper();

            return services;
        }
    }
}
