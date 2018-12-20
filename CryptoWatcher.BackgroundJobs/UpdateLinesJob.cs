using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Contexts;
using CryptoWatcher.Shared.Extensions;
using CryptoWatcher.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateLinesJob
    {
        private readonly IContext _context;
        private readonly ILogger<UpdateLinesJob> _logger;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<Line> _lineRepository;

        public UpdateLinesJob(
            IContext context,
            ILogger<UpdateLinesJob> logger,
            IRepository<Currency> currencyRepository,
            IRepository<Indicator> indicatorRepository,
            IRepository<Watcher> watcherRepository,
            IRepository<Line> lineRepository)
        {
            _context = context;
            _logger = logger;
            _currencyRepository = currencyRepository;
            _indicatorRepository = indicatorRepository;
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

                // Get all watchers
                var watchers = await _watcherRepository.GetAll();

                // Build lines
                var lines = LineBuilder.BuildLines(currencies, indicators, watchers);

                // Set lines
                _lineRepository.AddRange(lines);

                // Save
                await _context.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(new
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
                _logger.LogSplunkError(ex);
            }
        }
    }
}