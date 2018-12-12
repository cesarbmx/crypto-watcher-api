using System;
using System.Diagnostics;
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
    public class UpdateWatchersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly ILogger<UpdateWatchersJob> _logger;
        private readonly CacheService _cacheService;
        public UpdateWatchersJob(
            MainDbContext mainDbContext,
            IRepository<Watcher> watcherRepository,
            ILogger<UpdateWatchersJob> logger,
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
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Get all watchers
                var watchers = await _watcherRepository.GetAll();

                // Get all default watchers
                var defaultWatchers = await _cacheService.GetFromCache<Watcher>(CacheKey.DefaultWatchers);

                // Sync watchers
                watchers.SyncWatchers(defaultWatchers);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stpo watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(new
                {
                    watchers.Count,
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