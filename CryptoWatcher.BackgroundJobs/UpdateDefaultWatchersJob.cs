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
    public class UpdateDefaultWatchersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateDefaultWatchersJob> _logger;
        public UpdateDefaultWatchersJob(
            MainDbContext mainDbContext,
            ILogger<UpdateDefaultWatchersJob> logger)
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

                // Get newst time
                var newestTime = await _mainDbContext.Lines.MaxAsync(x => x.Time);

                // Get current lines
                var currentLines = await _mainDbContext.Lines.Where(LineExpression.CurrentLine(newestTime)).ToListAsync();

                // Build default watchers
                var newDefaultWatchers = WatcherBuilder.BuildDefaultWatchers(currentLines);

                // Get all default watchers
                var defaultWatchers = await _mainDbContext.Watchers.Where(WatcherExpression.DefaultWatcher()).ToListAsync();

                // Update 
                _mainDbContext.UpdateCollection(defaultWatchers, newDefaultWatchers);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkJob(new
                {
                    newDefaultWatchers.Count,
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