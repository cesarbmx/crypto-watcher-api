using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Domain.Services
{
    public class WatcherService
    {
        private readonly IWatcherRepository _watcherRepository;

        public WatcherService(IWatcherRepository watcherRepository)
        {
            _watcherRepository = watcherRepository;
        }

        public async Task<List<Watcher>> GetWatcher()
        {
            // Get Watcher
            return await _watcherRepository.GetAll();
        }
        public async Task<Watcher> GetWatcher(string watcherId)
        {
            // Get Watcher by key
            var watcher = await _watcherRepository.GetById(watcherId);

            // Throw NotFound exception if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessages.NotFound);

            // Return
            return watcher;
        }
        public void Watcher(Watcher watcher)
        {
            // Add watcher
            _watcherRepository.Add(watcher);
        }
    }
}
