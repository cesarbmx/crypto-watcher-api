using AutoMapper;
using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Application.Mappers
{
    public class LineMapper : Profile
    {
        public LineMapper()
        {
            CreateMap<Line, Responses.Line>();
        }
    }
}
