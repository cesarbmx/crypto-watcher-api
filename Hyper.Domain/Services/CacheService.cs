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

        public async Task<List<T>> GetFromCache<T>()
        {
            // Get cache
            var cache = await _cacheRepository.GetByKey(typeof(T).Name);

            // Return
            if (cache == null) return new List<T>();
            return cache.GetValue<T>();
        }
        public async Task SetInCache<T>(List<T> value)
        {
            // Set cache
            var cache = await _cacheRepository.GetByKey(typeof(T).Name);
            if (cache == null)
            {
                // Add if it does not exist
                cache = new Cache();
                cache.SetValue(value);
                _cacheRepository.Add(cache);
            }
            else
            {
                // Update if it does exist
                cache.SetValue(value);
                _cacheRepository.Update(cache);
            }
        }
    }
}
