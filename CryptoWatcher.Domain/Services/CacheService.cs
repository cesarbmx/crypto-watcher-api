using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;

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
            return cache.Get<T>();
        }
        public Task SetInCache<T>(CacheKey key, List<T> value)
        {
            // Set cache
            var cache = CacheBuilder.BuildCache(key, value);
            _cacheRepository.Update(cache);

            // Return
            return Task.CompletedTask;
        }
    }
}
