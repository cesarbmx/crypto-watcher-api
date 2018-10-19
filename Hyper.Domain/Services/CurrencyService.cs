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

        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            // Get all currencies
            return await _cacheService.GetFromCache<Currency>();
        }
        public async Task SetAllCurrencies(IList<Currency> currencies)
        {
            // Set all currencies
            await _cacheService.SetInCache(currencies);
        }
    }
}
