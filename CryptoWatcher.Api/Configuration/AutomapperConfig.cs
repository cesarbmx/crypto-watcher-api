using System.Linq;
using AutoMapper;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.Shared.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.Configuration
{
    public static class AutomapperConfig
    {
        public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(

                 cfg =>
                 {
                     // Responses
                    cfg.CreateMap<Currency, CurrencyResponse>();
                     cfg.CreateMap<AuditLog, AuditLogResponse>();
                     cfg.CreateMap<Watcher, WatcherResponse>();
                     cfg.CreateMap<User, UserResponse>();
                     cfg.CreateMap<Notification, NotificationResponse>();
                     cfg.CreateMap<Order, OrderResponse>();
                     cfg.CreateMap<Indicator, IndicatorResponse>()
                         .ForMember(dest => dest.Dependencies, opt => opt.MapFrom(src => src.Dependencies.Select(x => x.DependencyId).ToArray()));
                     cfg.CreateMap<Line, LineResponse>();
                     cfg.CreateMap<LineChart, LineChartResponse>();

                 }, typeof(Startup));

            return services;
        }
    }
}
