using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Automapper
{
    public class LineMapping : Profile
    {
        public LineMapping()
        {
            CreateMap<Line, Resources.Line>();
        }
    }
}
