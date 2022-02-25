using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class IndicatorMapping : Profile
    {
        public IndicatorMapping()
        {
            CreateMap<Indicator, Responses.Indicator>();
        }
    }
}
