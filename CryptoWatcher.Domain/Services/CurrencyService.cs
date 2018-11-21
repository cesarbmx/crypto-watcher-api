using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Domain.Services
{
    public class CurrencyService
    {
        private readonly CacheService _cacheService;

        public CurrencyService(CacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<List<Currency>> GetCurrencies()
        {
            // Get currencies
            return await _cacheService.GetFromCache<Currency>();
        }
        public async Task<Currency> GetCurrency(string id)
        {
            // Get currencies
            var allCurrencies = await GetCurrencies();

            // Pick the required currency from the previous list
            var currency = allCurrencies.FirstOrDefault(x => x.Id == id);

            // Throw NotFound exception if the currency does not exist
            if (currency == null) throw new NotFoundException(CurrencyMessages.NotFound);

            // Return
            return currency;

        }
        public async Task SetAllCurrencies(List<Currency> currencies)
        {
            // Set all currencies
            await _cacheService.SetInCache(currencies);
        }
    }
}
