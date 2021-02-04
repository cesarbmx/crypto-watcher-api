using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Logging.Extensions;
using CesarBmx.Shared.Persistence.Extensions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class CurrencyService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrencyService> _logger;
        private readonly CoinpaprikaAPI.Client _coinpaprikaClient;

        public CurrencyService(
            MainDbContext mainDbContext,
            IMapper mapper,
            ILogger<CurrencyService> logger,
            CoinpaprikaAPI.Client coinpaprikaClient)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _logger = logger;
            _coinpaprikaClient = coinpaprikaClient;
        }

        public async Task<List<Responses.Currency>> GetAllCurrencies()
        {
            // Get all currencies
            var currencies = await _mainDbContext.Currencies.ToListAsync();

            // Response
            var response = _mapper.Map<List<Responses.Currency>>(currencies);

            // Return
            return response;
        }
        public async Task<Responses.Currency> GetCurrency(string currencyId)
        {
            // Get currency
            var currency = await _mainDbContext.Currencies.FindAsync(currencyId);

            // Throw NotFound if it does not exist
            if (currency == null) throw new NotFoundException(CurrencyMessage.CurrencyNotFound);

            // Response
            var response = _mapper.Map<Responses.Currency>(currency);

            // Return
            return response;
        }
        public async Task<List<Currency>> UpdateCurrencies()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get all currencies from CoinMarketCap
            var result = await _coinpaprikaClient.GetTickersAsync();

            var tickers = result.Value.Where(x =>
                x.Id == "btc-bitcoin" ||
                x.Id == "xrp-xrp" ||
                x.Id == "eth-ethereum" ||
                x.Id == "bch-bitcoin-cash" ||
                x.Id == "xlm-stellar" ||
                x.Id == "eos-eos" ||
                x.Id == "ada-cardano").ToList();

            // Build currencies
            var newCurrencies = _mapper.Map<List<Currency>>(tickers);

            // Get all currencies
            var currencies = await _mainDbContext.Currencies.ToListAsync();

            // Update 
            _mainDbContext.UpdateCollection(currencies, newCurrencies);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation(nameof(UpdateCurrencies), new
            {
                newCurrencies.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return
            return newCurrencies;
        }
    }
}
