using AutoMapper;
using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Application.Mappers
{
    public class IndicatorDependencyMapper : Profile
    {
        public IndicatorDependencyMapper()
        {
            CreateMap<IndicatorDependency, Responses.IndicatorDependency>()
                .ForMember(dest => dest.IndicatorId, opt => opt.MapFrom(src => src.Dependency.IndicatorId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Dependency.UserId))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Dependency.Abbreviation));
        }
    }
}
