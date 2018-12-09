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

        public async Task<List<T>> GetFromCache<T>()
        {
            // Get cache
            var cache = await _cacheRepository.GetSingle(typeof(T).Name);

            // Return
            return cache.GetValue<T>();
        }
        public Task SetInCache<T>(List<T> value)
        {
            var cache = new Cache().SetValue(value);
            _cacheRepository.Update(cache);

            return Task.CompletedTask;
        }
    }
}
