using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Automapper
{
    public class IndicatorDependencyMapping : Profile
    {
        public IndicatorDependencyMapping()
        {
            CreateMap<IndicatorDependency, Responses.IndicatorDependency>()
                .ForMember(dest => dest.IndicatorId, opt => opt.MapFrom(src => src.Indicator.IndicatorId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Indicator.UserId))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Indicator.Abbreviation));
        }
    }
}
