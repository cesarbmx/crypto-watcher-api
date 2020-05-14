using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class LineChartMapping : Profile
    {
        public LineChartMapping()
        {
            CreateMap<LineChart, LineChartResponse>();
        }
    }
}
