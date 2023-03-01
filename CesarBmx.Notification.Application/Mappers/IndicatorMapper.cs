using AutoMapper;
using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Application.Mappers
{
    public class IndicatorMapper : Profile
    {
        public IndicatorMapper()
        {
            CreateMap<Indicator, Responses.Indicator>();
        }
    }
}
