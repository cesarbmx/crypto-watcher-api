using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class ChartMapping : Profile
    {
        public ChartMapping()
        {
            CreateMap<Chart, Responses.Chart>();
        }
    }
}
