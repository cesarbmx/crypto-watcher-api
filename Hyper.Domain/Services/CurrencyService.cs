using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;

namespace Hyper.Domain.Services
{
    public class CurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<IEnumerable<Currency>> GetAllSpikingCurrencies()
        {
            // Get all currencies
            var currencies = await _currencyRepository.GetAllCurrencies();

            // Return
            return currencies;
        }
    }
}
