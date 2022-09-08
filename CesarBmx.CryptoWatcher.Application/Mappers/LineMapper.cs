using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class LineMapper : Profile
    {
        public LineMapper()
        {
            CreateMap<Line, Responses.Line>();
        }
    }
}
