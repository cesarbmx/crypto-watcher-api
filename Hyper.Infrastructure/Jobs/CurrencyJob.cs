using System;
using System.Collections.Generic;
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
        readonly ILogger<CurrencyJob> _logger;
        private readonly MainDbContext _mainDbContext;
        private readonly ICoinMarketCapClient _coinMarketCapClient;
        private readonly CacheService _cacheService;
        private readonly LogService _logService;

        public CurrencyJob(
            IMapper mapper,
            ILogger<CurrencyJob> logger,
            MainDbContext mainDbContext,
            ICoinMarketCapClient coinMarketCapClient,
            CacheService cacheService,
            LogService logService)
        {
            _mapper = mapper;
            _logger = logger;
            _mainDbContext = mainDbContext;
            _coinMarketCapClient = coinMarketCapClient;
            _cacheService = cacheService;
            _logService = logService;
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
                await _cacheService.SetInCache(currencies);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log
                _logService.LogEvent(Event.CurrenciesUpdated);

                // Log into Splunk
                _logger.LogInformation("Event=ImportCountriesCompleted", Event.CurrenciesUpdated);
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogError(ex, "Event=ImportCountriesFailed");
            }
        }
    }
}