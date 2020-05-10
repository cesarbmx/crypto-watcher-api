using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;
using CoinMarketCap;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class CurrencyService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IMapper _mapper;
        private readonly Logger<CurrencyService> _logger;
        private readonly CoinMarketCapClient _coinMarketCapClient;

        public CurrencyService(
            MainDbContext mainDbContext,
            IRepository<Currency> currencyRepository,
            IMapper mapper,
            Logger<CurrencyService> logger,
            CoinMarketCapClient coinMarketCapClient)
        {
            _mainDbContext = mainDbContext;
            _currencyRepository = currencyRepository;
            _mapper = mapper;
            _logger = logger;
            _coinMarketCapClient = coinMarketCapClient;
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

            // Throw NotFoundException if it does not exist
            if (currency == null) throw new NotFoundException(CurrencyMessage.CurrencyNotFound);

            // Response
            var response = _mapper.Map<CurrencyResponse>(currency);

            // Return
            return response;
        }
        public async Task UpdateCurrencies()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Time
            var time = DateTime.Now;

            // Get all currencies from CoinMarketCap
            var result = await _coinMarketCapClient.GetTickerListAsync(10);
            result = result.Where(x =>
                x.Id == "bitcoin" ||
                x.Id == "ripple" ||
                x.Id == "ethereum" ||
                x.Id == "bitcoin-cash" ||
                x.Id == "stellar" ||
                x.Id == "eos" ||
                x.Id == "cardano").ToList();

            // Build currencies
            var newCurrencies = _mapper.Map<List<Currency>>(result);

            // Get all currencies
            var currencies = await _currencyRepository.GetAll();

            // Update 
            _currencyRepository.UpdateCollection(currencies, newCurrencies, time);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation(new
            {
                currencies.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });
        }
    }
}
