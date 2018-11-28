using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoinMarketCap.Core;
using Hangfire;
using CryptoWatcher.Domain.Models;
using Microsoft.Extensions.Logging;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;

namespace CryptoWatcher.BackgroundJobs
{
    public class ImportCurrenciesJob
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ImportCurrenciesJob> _logger;
        private readonly MainDbContext _mainDbContext;
        private readonly ICoinMarketCapClient _coinMarketCapClient;
        private readonly CurrencyService _currencyService;


        public ImportCurrenciesJob(
            IMapper mapper,
            ILogger<ImportCurrenciesJob> logger,
            MainDbContext mainDbContext,
            ICoinMarketCapClient coinMarketCapClient,
            CurrencyService currencyService)
        {
            _mapper = mapper;
            _logger = logger;
            _mainDbContext = mainDbContext;
            _coinMarketCapClient = coinMarketCapClient;
            _currencyService = currencyService;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Execute()
        {
            try
            {
                // Get currencies from CoinMarketCap
                var result = await _coinMarketCapClient.GetTickerListAsync(10);
                result = result.Where(x => 
                    x.Id == "bitcoin" ||
                    x.Id == "ripple" ||
                    x.Id == "ethereum" ||
                    x.Id == "bitcoin-cash" ||
                    x.Id == "stellar" ||
                    x.Id == "eos" ||
                    x.Id == "cardano").ToList();

                // Map to our Model
                var currencies = _mapper.Map<List<Currency>>(result);

                // Set all currencies
                await _currencyService.SetCurrencies(currencies.ToList());

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log into Splunk               
                _logger.LogSplunkInformation(nameof(LoggingEvents.CurrenciesImported));
            }
            catch (Exception ex)
            {
                // Log into Splunk              
                _logger.LogSplunkError(nameof(LoggingEvents.ImportingCurrenciesFailed), ex.Message);
            }
        }
    }
}