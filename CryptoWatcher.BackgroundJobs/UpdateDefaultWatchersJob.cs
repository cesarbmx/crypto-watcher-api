using System;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateDefaultWatchersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly ILogger<UpdateOrdersJob> _logger;
        private readonly CacheService _cacheService;
        public UpdateDefaultWatchersJob(
            MainDbContext mainDbContext,
            IRepository<Indicator> indicatorRepository,
            ILogger<UpdateOrdersJob> logger,
            CacheService cacheService)
        {
            _mainDbContext = mainDbContext;
            _indicatorRepository = indicatorRepository;
            _logger = logger;
            _cacheService = cacheService;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Get all currencies
                var currencies = await _cacheService.GetFromCache<Currency>(CacheKey.Currencies);

                // Get indicators
                var indicators = await _indicatorRepository.GetAll();

                // Get all default watchers
                var defaultWatchers = WatcherBuilder.BuildDefaultWatchers(currencies, indicators);

                // Set default watchers
                await _cacheService.SetInCache(CacheKey.DefaultWatchers, defaultWatchers);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log into Splunk
                _logger.LogSplunkInformation();

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
               // Log into Splunk 
                _logger.LogSplunkError(ex);
            }
        }
    }
}