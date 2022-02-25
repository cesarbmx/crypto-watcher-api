using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class LineMapping : Profile
    {
        public LineMapping()
        {
            CreateMap<Line, Responses.Line>();
        }
    }
}
