using System.Linq;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class IndicatorMapping : Profile
    {
        public IndicatorMapping()
        {
            CreateMap<Indicator, IndicatorResponse>()
                .ForMember(dest => dest.Dependencies, opt => opt.MapFrom(src => src.Dependencies.Select(x => x.DependencyId).ToArray()));
        }
    }
}
