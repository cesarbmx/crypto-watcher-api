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
    public class UpdateLinesJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateLinesJob> _logger;

        public UpdateLinesJob(
            MainDbContext mainDbContext,
            ILogger<UpdateLinesJob> logger)
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

                // Get all currencies
                var currencies = await _mainDbContext.Currencies.ToListAsync();

                // Get all indicators
                var indicators = await _mainDbContext.Indicators.ToListAsync();

                // Get all indicator dependencies
                var indicatorDependencies = await _mainDbContext.IndicatorDependencies.ToListAsync();

                // Build dependency level
                IndicatorBuilder.BuildDependencyLevel(indicators, indicatorDependencies);

                // Get non-default watchers with buy or sell
                var watchers = await _mainDbContext.Watchers.Where(WatcherExpression.WatcherWillingToBuyOrSell()).ToListAsync();

                // Build new lines
                var lines = LineBuilder.BuildLines(currencies, indicators, watchers, DateTime.Now);

                // Set new lines
                _mainDbContext.Lines.AddRange(lines);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkJob(new
                {
                    lines.Count,
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