using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class RemoveOldLinesJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<RemoveOldLinesJob> _logger;
        private readonly IRepository<DataPoint> _lineRepository;

        public RemoveOldLinesJob(
            MainDbContext mainDbContext,
            ILogger<RemoveOldLinesJob> logger,
            IRepository<DataPoint> lineRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _lineRepository = lineRepository;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Get old lines
                var lines = await _lineRepository.GetAll(LineExpression.OldLine());

                // Remove
                _lineRepository.RemoveRange(lines);

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
                    Failed = ex.Message
                });
                _logger.LogSplunkError(ex);
            }
        }
    }
}