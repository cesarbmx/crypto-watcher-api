using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoinMarketCap.Core;
using Hangfire;
using Hyper.Domain.Models;
using Microsoft.Extensions.Logging;
using Hyper.Domain.Services;
using Hyper.Persistence.Contexts;

namespace Hyper.Api.Jobs
{
    public class ImportCurrenciesJob
    {
        private readonly IMapper _mapper;
        readonly ILogger<ImportCurrenciesJob> _logger;
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
                // Get all currencies from CoinMarketCap
                var result = await _coinMarketCapClient.GetTickerListAsync(5);

                // Map to our Model
                var currencies = _mapper.Map<List<Currency>>(result);

                // Set all currencies
                await _currencyService.SetAllCurrencies(currencies.ToList());

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log into Splunk
                _logger.LogInformation(LoggingEvents.AllCurrenciesHaveBeenImported,"All currencies have been imported");
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogError(LoggingEvents.ImportingAllCurrenciesHasFailed, ex, "Importing currencies has failed");
            }
        }
    }
}