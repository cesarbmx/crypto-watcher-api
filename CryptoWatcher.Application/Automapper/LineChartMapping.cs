using AutoMapper;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class LineChartMapping : Profile
    {
        public LineChartMapping()
        {
            CreateMap<LineChart, Responses.LineChart>();
        }
    }
}
