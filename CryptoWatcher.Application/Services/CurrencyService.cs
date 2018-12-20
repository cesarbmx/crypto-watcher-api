using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Application.Services
{
    public class CurrencyService
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IMapper _mapper;

        public CurrencyService(IRepository<Currency> currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        public async Task<List<CurrencyResponse>> GetAllCurrencies()
        {
            // Get all currencies
            var currencies = await _currencyRepository.GetAll();

            // Response
            var response = _mapper.Map<List<CurrencyResponse>>(currencies);

            // Return
            return response;
        }
        public async Task<CurrencyResponse> GetCurrency(string currencyId)
        {
            // Get currency
            var currency = await _currencyRepository.GetSingle(currencyId);

            // Throw NotFound exception if the currency does not exist
            if (currency == null) throw new NotFoundException(CurrencyMessage.CurrencyNotFound);

            // Response
            var response = _mapper.Map<CurrencyResponse>(currency);

            // Return
            return response;
        }
    }
}
