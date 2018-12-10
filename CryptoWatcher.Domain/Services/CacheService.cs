using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;

namespace CryptoWatcher.Domain.Services
{
    public class CacheService
    {
        private readonly IRepository<Cache> _cacheRepository;

        public CacheService(IRepository<Cache> cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<List<T>> GetFromCache<T>(CacheKey key)
        {
            // Get cache
            var cache = await _cacheRepository.GetSingle(key.ToString());

            // Return
            return cache.Get<T>(key);
        }
        public Task SetInCache<T>(CacheKey key, List<T> value)
        {
            // Set cache
            var cache = new Cache().Set(key, value);
            _cacheRepository.Update(cache);

            // Return
            return Task.CompletedTask;
        }
    }
}
