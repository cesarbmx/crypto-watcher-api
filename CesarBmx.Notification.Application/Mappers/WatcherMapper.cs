using AutoMapper;
using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Application.Mappers
{
    public class WatcherMapper : Profile
    {
        public WatcherMapper()
        {
            CreateMap<Watcher, Responses.Watcher>();
        }
    }
}
