using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Application.Services
{
    public class CurrencyService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public CurrencyService(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }

        public async Task<List<CurrencyResponse>> GetAllCurrencies()
        {
            // Get all currencies
            var currencies = await _mainDbContext.Currencies.ToListAsync();

            // Response
            var response = _mapper.Map<List<CurrencyResponse>>(currencies);

            // Return
            return response;
        }
        public async Task<CurrencyResponse> GetCurrency(string currencyId)
        {
            // Get currency
            var currency = await _mainDbContext.Currencies.FindAsync(currencyId);

            // Throw NotFoundException if it does not exist
            if (currency == null) throw new NotFoundException(CurrencyMessage.CurrencyNotFound);

            // Response
            var response = _mapper.Map<CurrencyResponse>(currency);

            // Return
            return response;
        }
    }
}
