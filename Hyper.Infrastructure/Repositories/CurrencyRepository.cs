using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;
using Hyper.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Hyper.Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private MainDbContext _mainDbContext;
        private IEnumerable<Currency> _currencies;

        public CurrencyRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
            _currencies = new List<Currency>();
        }

        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            //return await _mainDbContext.Currencies.ToListAsync(); // TODO: (Cesar) Remove in memory list
            // Get  all currencies
            return await Task.FromResult(_currencies);
        }
        public async Task SetAllCurrencies(IEnumerable<Currency> currencies)
        {
            // Set all currencies
            _currencies = currencies;
            await Task.CompletedTask;
        }
    }
}
