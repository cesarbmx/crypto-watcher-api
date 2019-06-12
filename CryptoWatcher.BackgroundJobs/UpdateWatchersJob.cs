using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateWatchersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateWatchersJob> _logger;
        public UpdateWatchersJob(
            MainDbContext mainDbContext,
            ILogger<UpdateWatchersJob> logger)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
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
                var watchers = await _mainDbContext.Watchers.ToListAsync();

                // Get all default watchers
                var defaultWatchers = await _mainDbContext.Watchers.Where(WatcherExpression.DefaultWatcher()).ToListAsync();

                // Sync watchers
                watchers.SyncWatchers(defaultWatchers);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkJob(new
                {
                    watchers.Count,
                    ExecutionTime = stopwatch.Elapsed.TotalSeconds
                });

                // Return
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log into Splunk 
                _logger.LogSplunkJob(new
                {
                    JobFailed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}