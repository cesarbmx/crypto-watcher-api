using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Hyper.Api.Responses;
using Hyper.Domain.Models;
using Hyper.Shared.Extensions;

namespace Hyper.Api.Configuration
{
    public static class AutomapperConfig
    {
        public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(

                cfg => {
                    
                    // Api
                    cfg.CreateMap<Version, VersionResponse>();
                    cfg.CreateMap<Health, HealthResponse>();
                    cfg.CreateMap<Version, VersionResponse>()
                        .ForMember(dest => dest.BuildDateTime, opt => opt.MapFrom(src => src.LastBuild.ToString("yyyy/MM/dd HH:mm")))
                        .ForMember(dest => dest.LastBuildOccurred, opt => opt.MapFrom(src => src.LastBuild.DaysHoursMinutesAndSecondsSinceDate()));
                    cfg.CreateMap<Health, HealthResponse>();
                    cfg.CreateMap<Currency, CurrencyResponse>();

                    // Infrastructure
                    cfg.AddProfile(new Infrastructure.Configuration.AutomapperConfig());
                    
                });

            return services;
        }
    }
}
