using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;

namespace Hyper.Domain.Services
{
    public class CurrencyService
    {
        private readonly CacheService _cacheService;

        public CurrencyService(CacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<Currency>> GetAllSpikingCurrencies()
        {
            // Get all currencies
            return await _cacheService.GetCache<Currency>();
        }
    }
}
