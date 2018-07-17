using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;

namespace Hyper.Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly CacheRepository _cacheRepository;

        public CurrencyRepository(CacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            // Get  all currencies
            return await _cacheRepository.GetByKey<Currency>();
        }
        public async Task SetAllCurrencies(IEnumerable<Currency> currencies)
        {
            // Set all currencies
            await _cacheRepository.Set(currencies);
        }
    }
}
