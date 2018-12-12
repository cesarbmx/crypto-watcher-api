using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateDefaultWatchersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateDefaultWatchersJob> _logger;
        private readonly CacheService _cacheService;
        public UpdateDefaultWatchersJob(
            MainDbContext mainDbContext,
            ILogger<UpdateDefaultWatchersJob> logger,
            CacheService cacheService)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _cacheService = cacheService;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Get all lines
                var lines = await _cacheService.GetFromCache<Line>(CacheKey.Lines);

                // Build default watchers
                var defaultWatchers = WatcherBuilder.BuildDefaultWatchers(lines);

                // Set default watchers
                await _cacheService.SetInCache(CacheKey.DefaultWatchers, defaultWatchers);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stpo watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(new
                {
                    defaultWatchers.Count,
                    stopwatch.Elapsed.TotalSeconds
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