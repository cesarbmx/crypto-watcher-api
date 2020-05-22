using AutoMapper;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class WatcherMapping : Profile
    {
        public WatcherMapping()
        {
            CreateMap<Watcher, Responses.Watcher>();
        }
    }
}
