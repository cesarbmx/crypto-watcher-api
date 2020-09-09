using AutoMapper;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class ChartMapping : Profile
    {
        public ChartMapping()
        {
            CreateMap<Chart, Responses.Chart>();
        }
    }
}
