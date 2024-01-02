using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class ChartMapper : Profile
    {
        public ChartMapper()
        {
            CreateMap<Chart, Responses.ChartResponse>();
        }
    }
}
