using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hyper.Domain.Messages;
using Hyper.Domain.Models;
using Hyper.Shared.Exceptions;

namespace Hyper.Domain.Services
{
    public class CurrencyService
    {
        private readonly CacheService _cacheService;

        public CurrencyService(CacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<List<Currency>> GetAllCurrencies()
        {
            // Get all currencies
            return await _cacheService.GetFromCache<Currency>();
        }
        public async Task<Currency> GetCurrency(string id)
        {
            // Get all currencies
            var allCurrencies = await GetAllCurrencies();

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
