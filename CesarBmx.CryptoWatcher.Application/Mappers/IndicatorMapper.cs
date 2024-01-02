using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class IndicatorMapper : Profile
    {
        public IndicatorMapper()
        {
            CreateMap<Indicator, Responses.IndicatorResponse>();
        }
    }
}
