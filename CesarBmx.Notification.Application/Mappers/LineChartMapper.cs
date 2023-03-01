using AutoMapper;
using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Application.Mappers
{
    public class ChartMapper : Profile
    {
        public ChartMapper()
        {
            CreateMap<Chart, Responses.Chart>();
        }
    }
}
