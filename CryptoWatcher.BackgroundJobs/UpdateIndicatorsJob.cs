using System;
using System.Diagnostics;
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
    public class UpdateIndicatorsJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateIndicatorsJob> _logger;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<IndicatorDependency> _indicatorDependencyRepository;

        public UpdateIndicatorsJob(
            MainDbContext mainDbContext,
            ILogger<UpdateIndicatorsJob> logger,
            IRepository<Indicator> indicatorRepository,
            IRepository<IndicatorDependency> indicatorDependencyRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _indicatorRepository = indicatorRepository;
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

                // Time
                var time = DateTime.Now;

                // Get all indicators
                var indicators = await _indicatorRepository.GetAll();

                // Get all indicator dependencies
                var indicatorDependencies = await _indicatorDependencyRepository.GetAll();

                // Build indicator dependencies
                IndicatorBuilder.BuildDependencies(indicators, indicatorDependencies);

                // Build dependency levels
                IndicatorBuilder.BuildDependencyLevels(indicators, indicatorDependencies);

                // Update
                _indicatorRepository.UpdateRange(indicators, time);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Build max dependency level
                var maxDependencyLevel  = IndicatorBuilder.BuildMaxDependencyLevel(indicators);

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkJob(new
                {
                    MaxLevel = maxDependencyLevel,
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