using AutoMapper;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class LineMapping : Profile
    {
        public LineMapping()
        {
            CreateMap<Line, Responses.Line>();
        }
    }
}
