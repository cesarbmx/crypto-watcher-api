using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class WatcherMapper : Profile
    {
        public WatcherMapper()
        {
            CreateMap<Watcher, Responses.Watcher>();
        }
    }
}
