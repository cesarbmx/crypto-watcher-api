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

        public async Task<IEnumerable<T>> GetFromCache<T>()
        {
            // Get cache
            var cache = await _cacheRepository.GetByKey(typeof(T).Name);

            // Return
            if (cache == null) return new List<T>();
            return cache.GetValue<T>();
        }
        public async Task SetInCache<T>(IEnumerable<T> value)
        {
            // Set cache
            var cache = await _cacheRepository.GetByKey(typeof(T).Name);
            if (cache == null)
            {
                cache = new Cache();
                cache.SetValue(value);
                _cacheRepository.Add(cache);
            }
            else
            {
                cache.SetValue(value);
            }
        }
    }
}
