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
    public class UpdateCurrenciesJob
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCurrenciesJob> _logger;
        private readonly MainDbContext _mainDbContext;
        private readonly ICoinMarketCapClient _coinMarketCapClient;
        private readonly CacheService _cacheService;


        public UpdateCurrenciesJob(
            IMapper mapper,
            ILogger<UpdateCurrenciesJob> logger,
            MainDbContext mainDbContext,
            ICoinMarketCapClient coinMarketCapClient,
            CacheService cacheService)
        {
            _mapper = mapper;
            _logger = logger;
            _mainDbContext = mainDbContext;
            _coinMarketCapClient = coinMarketCapClient;
            _cacheService = cacheService;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
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
                var currencies = _mapper.Map<List<Currency>>(result);

                // Set currencies
                await _cacheService.SetInCache(CacheKey.Currencies, currencies);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log into Splunk
                _logger.LogSplunkInformation(new
                {
                    CurrenciesImported = currencies.Count
                });
            }
            catch (Exception ex)
            {
                // Log into Splunk              
                _logger.LogSplunkError(ex);
            }
        }
    }
}