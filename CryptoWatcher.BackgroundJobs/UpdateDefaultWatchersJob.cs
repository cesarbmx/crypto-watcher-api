using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateDefaultWatchersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateDefaultWatchersJob> _logger;
        private readonly IRepository<Line> _lineRepository;
        private readonly IRepository<Watcher> _watcherRepository;
        public UpdateDefaultWatchersJob(
            MainDbContext mainDbContext,
            ILogger<UpdateDefaultWatchersJob> logger,
            IRepository<Line> lineRepository,
            IRepository<Watcher> watcherRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _lineRepository = lineRepository;
            _watcherRepository = watcherRepository;

        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Get all lines
                var lines = await _lineRepository.GetAll();

                // Build default watchers
                var newDefaultWatchers = WatcherBuilder.BuildDefaultWatchers(lines);

                // Update
                var watchers = await _watcherRepository.GetAll(WatcherExpression.DefaultWatcher());
                _watcherRepository.RemoveRange(watchers);
                _watcherRepository.AddRange(newDefaultWatchers);
              

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(new
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
                _logger.LogSplunkError(ex);
            }
        }
    }
}