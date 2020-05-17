using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class LineMapping : Profile
    {
        public LineMapping()
        {
            CreateMap<Line, LineResponse>();
        }
    }
}
