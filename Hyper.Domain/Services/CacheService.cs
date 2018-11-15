using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;

namespace Hyper.Domain.Services
{
    public class CacheService
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly LogService _logService;

        public CacheService(ICacheRepository cacheRepository, LogService logService)
        {
            _cacheRepository = cacheRepository;
            _logService = logService;
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

                // Add log
                _logService.Log(new Log(cache, "Add"));
            }
            else
            {
                // Update if it does exist
                cache.SetValue(value);

                // Add log
                _logService.Log(new Log(cache, "Update"));
            }
        }
    }
}
