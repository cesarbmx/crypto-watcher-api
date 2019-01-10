using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoinMarketCap.Core;
using CryptoWatcher.Domain.Builders;
using Hangfire;
using CryptoWatcher.Domain.Models;
using Microsoft.Extensions.Logging;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Extensions;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateCurrenciesJob
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCurrenciesJob> _logger;
        private readonly MainDbContext _mainDbContext;
        private readonly ICoinMarketCapClient _coinMarketCapClient;
        private readonly IRepository<Currency> _currencyRepository;

        public UpdateCurrenciesJob(
            IMapper mapper,
            ILogger<UpdateCurrenciesJob> logger,
            MainDbContext mainDbContext,
            ICoinMarketCapClient coinMarketCapClient,
            IRepository<Currency> currencyRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _mainDbContext = mainDbContext;
            _coinMarketCapClient = coinMarketCapClient;
            _currencyRepository = currencyRepository;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

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

                // Add 
                _currencyRepository.AddRange(EntityBuilder.BuildEntitiesToAdd(currencies, newCurrencies));

                // Update 
                _currencyRepository.UpdateRange(EntityBuilder.BuildEntitiesToUpdate(currencies, newCurrencies));

                // Remove 
                _currencyRepository.RemoveRange(EntityBuilder.BuildEntitiesToRemove(currencies, newCurrencies));

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkJob(new
                {
                    currencies.Count,
                    ExecutionTime = stopwatch.Elapsed.TotalSeconds
                });
            }
            catch (Exception ex)
            {
                // Log into Splunk    
                _logger.LogSplunkJob(new
                {
                    JobFailed = ex.Message
                });
                _logger.LogSplunkError(ex);
            }
        }
    }
}