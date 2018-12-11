using System;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateIndicatorsJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateIndicatorsJob> _logger;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<Cache> _cacheRepository;
        public UpdateIndicatorsJob(
            MainDbContext mainDbContext,
            ILogger<UpdateIndicatorsJob> logger,
            IRepository<Indicator> indicatorRepository,
            IRepository<Cache> cacheRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _indicatorRepository = indicatorRepository;
            _cacheRepository = cacheRepository;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Get all currencies
                var cache = await _cacheRepository.GetSingle(typeof(Currency).Name);
                var currencies = cache.Get<Currency>(CacheKey.Currencies);

                // Get all indicators
                var indicators = await _indicatorRepository.GetAll();

                // Get all default watchers
                var defaultWatchers = WatcherBuilder.BuildDefaultWatchers(currencies, indicators);

                // Set default watchers
                cache = new Cache().Set(CacheKey.DefaultWatchers, defaultWatchers);
                _cacheRepository.Add(cache);

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