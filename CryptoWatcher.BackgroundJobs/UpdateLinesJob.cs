using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateLinesJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateLinesJob> _logger;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<IndicatorDependency> _indicatorDependencyRepository;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<DataPoint> _lineRepository;

        public UpdateLinesJob(
            MainDbContext mainDbContext,
            ILogger<UpdateLinesJob> logger,
            IRepository<Currency> currencyRepository,
            IRepository<Indicator> indicatorRepository,
            IRepository<IndicatorDependency> indicatorDependencyRepository,
            IRepository<Watcher> watcherRepository,
            IRepository<DataPoint> lineRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _currencyRepository = currencyRepository;
            _indicatorRepository = indicatorRepository;
            _indicatorDependencyRepository = indicatorDependencyRepository;
            _watcherRepository = watcherRepository;
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

                // Get all currencies
                var currencies = await _currencyRepository.GetAll();

                // Get all indicators
                var indicators = await _indicatorRepository.GetAll();

                // Get all indicators dependencies
                foreach (var indicator in indicators)
                {
                    var dependencies = await _indicatorDependencyRepository.GetAll(IndicatorDependencyExpression.IndicatorDependencyFilter(indicator.IndicatorId, null));
                    indicator.SetDependencies(dependencies);
                }

                // Get none default watchers with buy or sell
                var watchers = await _watcherRepository.GetAll(WatcherExpression.NonDefaultWatcherWithBuyOrSell());

                // Build lines
                var lines = LineBuilder.BuildLines(currencies, indicators, watchers);

                // Set lines
                _lineRepository.AddRange(lines);

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