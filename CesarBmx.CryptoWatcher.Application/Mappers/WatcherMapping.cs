using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class WatcherMapping : Profile
    {
        public WatcherMapping()
        {
            CreateMap<Watcher, Responses.Watcher>();
        }
    }
}
