using System;
using AutoMapper;
using CoinMarketCap.Entities;
using Microsoft.Extensions.DependencyInjection;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Extensions;
using Version = CryptoWatcher.Domain.Models.Version;

namespace CryptoWatcher.Api.Configuration
{
    public static class AutomapperConfig
    {
        public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(

                cfg => {
                    
                    // Responses
                    cfg.CreateMap<Version, VersionResponse>();
                    cfg.CreateMap<Health, HealthResponse>();
                    cfg.CreateMap<Version, VersionResponse>()
                        .ForMember(dest => dest.BuildDateTime, opt => opt.MapFrom(src => src.LastBuild.ToString("yyyy/MM/dd HH:mm")))
                        .ForMember(dest => dest.LastBuildOccurred, opt => opt.MapFrom(src => src.LastBuild.DaysHoursMinutesAndSecondsSinceDate()));
                    cfg.CreateMap<Health, HealthResponse>();
                    cfg.CreateMap<Currency, CurrencyResponse>();
                    cfg.CreateMap<Log, LogResponse>();
                    cfg.CreateMap<Watcher, WatcherResponse>();
                    cfg.CreateMap<User, UserResponse>();
                    cfg.CreateMap<Notification, NotificationResponse>();
                    cfg.CreateMap<Order, OrderResponse>();

                    // Others
                    cfg.CreateMap<TickerEntity, Currency>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src.PriceUsd)))
                        .ForMember(dest => dest.Volume24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.Volume24hUsd)))
                        .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => Convert.ToDecimal(src.MarketCapUsd)))
                        .ForMember(dest => dest.PercentageChange24H, opt => opt.MapFrom(src => Convert.ToDecimal(src.PercentChange24h)));
                });

            return services;
        }
    }
}
