using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;

namespace Hyper.Domain.Services
{
    public class CacheService
    {

        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<Cache> GetCache(string key)
        {
            // Get cache
            var cache = await _cacheRepository.GetByKey(key);

            // Return
            return cache;
        }
        public async Task SetCache<T>(string key, List<T> value)
        {
            // Set cache
            var cache = await _cacheRepository.GetByKey(key);
            if (cache == null)
            {
                cache = new Cache();
                cache.SetValue(key, value);
                _cacheRepository.Add(cache);
            }
            else
            {
                cache.SetValue(key, value);
            }
        }
    }
}
