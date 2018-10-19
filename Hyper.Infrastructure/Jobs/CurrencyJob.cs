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
using Hyper.Infrastructure.Contexts;

namespace Hyper.Infrastructure.Jobs
{
    public class CurrencyJob
    {
        private readonly IMapper _mapper;
        private readonly LogService _logService;
        readonly ILogger<CurrencyJob> _logger;
        private readonly MainDbContext _mainDbContext;
        private readonly ICoinMarketCapClient _coinMarketCapClient;
        private readonly CurrencyService _currencyService;


        public CurrencyJob(
            IMapper mapper,
            LogService logService,
            ILogger<CurrencyJob> logger,
            MainDbContext mainDbContext,
            ICoinMarketCapClient coinMarketCapClient,
            CurrencyService currencyService)
        {
            _mapper = mapper;
            _logService = logService;
            _logger = logger;
            _mainDbContext = mainDbContext;
            _coinMarketCapClient = coinMarketCapClient;
            _currencyService = currencyService;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Import()
        {
            try
            {
                // Get all currencies from CoinMarketCap
                var result = await _coinMarketCapClient.GetTickerListAsync(5);

                // Map to our Model
                var currencies = _mapper.Map<IEnumerable<Currency>>(result);

                // Set all currencies
                await _currencyService.SetAllCurrencies(currencies.ToList());

                // Log
                var log = new Log("ImportCountriesCompleted");
                _logService.Log(log);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log into Splunk
                _logger.LogInformation("Event=ImportCountriesCompleted");
            }
            catch (Exception ex)
            {
                // Log
                var log = new Log("ImportCountriesFailed");
                _logService.Log(log);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log into Splunk
                _logger.LogError(ex, "Event=ImportCountriesFailed");
            }
        }
    }
}