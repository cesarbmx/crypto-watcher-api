using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class RemoveLinesJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<RemoveLinesJob> _logger;

        public RemoveLinesJob(
            MainDbContext mainDbContext,
            ILogger<RemoveLinesJob> logger)
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

                // Get lines to be removed
                var lines = await _mainDbContext.Lines.Where(LineExpression.ObsoleteLine()).ToListAsync();

                // Remove
                _mainDbContext.Lines.RemoveRange(lines);

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