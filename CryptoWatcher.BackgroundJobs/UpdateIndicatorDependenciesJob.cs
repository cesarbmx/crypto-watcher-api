using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateIndicatorDependenciesJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateLinesJob> _logger;
        private readonly IRepository<IndicatorDependency> _indicatorDependencyRepository;

        public UpdateIndicatorDependenciesJob(
            MainDbContext mainDbContext,
            ILogger<UpdateLinesJob> logger,
            IRepository<IndicatorDependency> indicatorDependencyRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _indicatorDependencyRepository = indicatorDependencyRepository;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Get all indicator dependencies
                var indicatorDependencies = await _indicatorDependencyRepository.GetAll();

                // Build
                IndicatorDependencyBuilder.BuildDependencyLevel(indicatorDependencies);

                // Update
                _indicatorDependencyRepository.UpdateRange(indicatorDependencies);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(new
                {
                    MaxLevel = indicatorDependencies.Select(x=>x.Level).Max(),
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