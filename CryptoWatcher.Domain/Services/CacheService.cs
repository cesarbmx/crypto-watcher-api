using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;

namespace CryptoWatcher.Domain.Services
{
    public class CacheService
    {
        private readonly IRepository<Cache> _cacheRepository;
        private readonly IRepository<Log> _logRepository;

        public CacheService(IRepository<Cache> cacheRepository, IRepository<Log> logRepository)
        {
            _cacheRepository = cacheRepository;
            _logRepository = logRepository;
        }

        public async Task<List<T>> GetFromCache<T>()
        {
            // Get cache
            var cache = await _cacheRepository.GetById(typeof(T).Name);

            // Return
            if (cache == null) return new List<T>();
            return cache.GetValue<T>();
        }
        public async Task SetInCache<T>(List<T> value)
        {
            // Set cache
            var cache = await _cacheRepository.GetById(typeof(T).Name);
            if (cache == null)
            {
                // Add if it does not exist
                cache = new Cache();
                cache.SetValue(value);
                _cacheRepository.Add(cache);

                // Add log
                _logRepository.Add(new Log(cache, "Add"));
            }
            else
            {
                // Update if it exists
                cache.SetValue(value);

                // Add log
                _logRepository.Add(new Log(cache, "Update"));
            }
        }
    }
}
