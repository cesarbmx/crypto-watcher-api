using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateWatchersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly ILogger<UpdateOrdersJob> _logger;
        private readonly CacheService _cacheService;
        public UpdateWatchersJob(
            MainDbContext mainDbContext,
            IRepository<Watcher> watcherRepository,
            ILogger<UpdateOrdersJob> logger,
            CacheService cacheService)
        {
            _mainDbContext = mainDbContext;
            _watcherRepository = watcherRepository;
            _logger = logger;
            _cacheService = cacheService;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Get all watchers
                var watchers = await _watcherRepository.GetAll();

                // Get all lines
                var lines = await _cacheService.GetFromCache<Line>(CacheKey.Lines);

                // Sync watcher
                foreach (var watcher in watchers)
                {
                    var currencyId = watcher.CurrencyId;
                    var indicatorId = watcher.IndicatorId;
                    var line = lines.FirstOrDefault(x => x.CurrencyId == currencyId && x.IndicatorId == indicatorId);
                    if(line != null) watcher.Sync(line.Value, line.RecommendedBuy, line.RecommendedSell);
                }

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log into Splunk
                _logger.LogSplunkInformation(new
                {
                    WatchersUpdated = watchers.Count
                });

                // Return
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