using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Automapper
{
    public class WatcherMapping : Profile
    {
        public WatcherMapping()
        {
            CreateMap<Watcher, Resources.Watcher>();
        }
    }
}
