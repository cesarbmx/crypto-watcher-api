using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Automapper
{
    public class IndicatorMapping : Profile
    {
        public IndicatorMapping()
        {
            CreateMap<Indicator, Responses.Indicator>();
        }
    }
}
