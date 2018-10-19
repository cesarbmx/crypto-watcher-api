using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;

namespace Hyper.Domain.Services
{
    public class CurrencyService
    {
        private readonly CacheService _cacheService;
        private readonly LogService _logService;

        public CurrencyService(CacheService cacheService, LogService logService)
        {
            _cacheService = cacheService;
            _logService = logService;
        }

        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            // Get all currencies
            return await _cacheService.GetFromCache<Currency>();
        }
        public async Task SetAllCurrencies(IEnumerable<Currency> currencies)
        {
            // Set all currencies
            await _cacheService.SetInCache(currencies);

            // Log
            _logService.LogInfo(Event.CurrenciesImported);
        }
    }
}
