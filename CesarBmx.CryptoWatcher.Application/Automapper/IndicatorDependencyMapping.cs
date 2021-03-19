using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Automapper
{
    public class IndicatorDependencyMapping : Profile
    {
        public IndicatorDependencyMapping()
        {
            CreateMap<IndicatorDependency, Resources.IndicatorDependency>()
                .ForMember(dest => dest.IndicatorId, opt => opt.MapFrom(src => src.Dependency.IndicatorId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Dependency.UserId))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Dependency.Abbreviation));
        }
    }
}
