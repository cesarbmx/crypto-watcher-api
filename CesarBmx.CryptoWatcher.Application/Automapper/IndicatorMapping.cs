using System.Linq;
using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Automapper
{
    public class IndicatorMapping : Profile
    {
        public IndicatorMapping()
        {
            CreateMap<Indicator, Responses.Indicator>()
                .ForMember(dest => dest.Dependencies, opt => opt.MapFrom(src => src.Dependencies.Select(x => x.DependencyUserId + "."+ x.DependencyIndicatorId).ToArray()));
        }
    }
}
