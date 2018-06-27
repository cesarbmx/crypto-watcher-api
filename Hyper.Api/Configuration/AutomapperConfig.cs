using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Hyper.Api.Responses;
using Hyper.Domain.Models;
using Hyper.Shared.Extensions;

namespace Hyper.Api.Configuration
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Version, VersionResponse>();
            CreateMap<Health, HealthResponse>();
            CreateMap<Version, VersionResponse>()
                .ForMember(dest => dest.BuildDateTime,
                    opt => opt.MapFrom(src => src.LastBuild.ToString("yyyy/MM/dd HH:mm")))
                .ForMember(dest => dest.LastBuildOccurred,
                    opt => opt.MapFrom(src => src.LastBuild.DaysHoursMinutesAndSecondsSinceDate()));
            CreateMap<Health, HealthResponse>();
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
