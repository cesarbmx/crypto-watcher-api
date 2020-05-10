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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class CurrencyService
    {
        private readonly DbContext _dbContext;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrencyService> _logger;
        private readonly CoinpaprikaAPI.Client _coinpaprikaClient;

        public CurrencyService(
            DbContext dbContext,
            IRepository<Currency> currencyRepository,
            IMapper mapper,
            ILogger<CurrencyService> logger,
            CoinpaprikaAPI.Client coinpaprikaClient)
        {
            _dbContext = dbContext;
            _currencyRepository = currencyRepository;
            _mapper = mapper;
            _logger = logger;
            _coinpaprikaClient = coinpaprikaClient;
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
            var result = await _coinpaprikaClient.GetTickersAsync();
            var curencies = result.Value.Where(x =>
                x.Id == "btc-bitcoin" ||
                x.Id == "xrp-xrp" ||
                x.Id == "eth-ethereum" ||
                x.Id == "bch-bitcoin-cash" ||
                x.Id == "xlm-stellar" ||
                x.Id == "eos-eos" ||
                x.Id == "ada-cardano").ToList();

            // Build currencies
            var newCurrencies = _mapper.Map<List<Currency>>(curencies);

            // Get all currencies
            var currencies = await _currencyRepository.GetAll();

            // Update 
            _currencyRepository.UpdateCollection(currencies, newCurrencies, time);

            // Save
            await _dbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("UpdateCurrencies", new
            {
                newCurrencies.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });
        }
    }
}
