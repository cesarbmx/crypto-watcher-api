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
    public class RemoveLinesJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<RemoveLinesJob> _logger;
        private readonly IRepository<Line> _chartRepository;

        public RemoveLinesJob(
            MainDbContext mainDbContext,
            ILogger<RemoveLinesJob> logger,
            IRepository<Line> chartRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _chartRepository = chartRepository;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Get all charts
                var charts = await _chartRepository.GetAll(LineExpression.ObsoleteLine());

                // Remove
                _chartRepository.RemoveRange(charts);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(new
                {
                    charts.Count,
                    ExecutionTime = stopwatch.Elapsed.TotalSeconds
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