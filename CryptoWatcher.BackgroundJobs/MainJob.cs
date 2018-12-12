using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Shared.Extensions;
using Hangfire;
using Microsoft.Extensions.Logging;


namespace CryptoWatcher.BackgroundJobs
{
    public class MainJob
    {
        private readonly UpdateCurrenciesJob _updateCurrenciesJob;
        private readonly UpdateLinesJob _updateLinesJob;
        private readonly UpdateDefaultWatchersJob _updateDefaultWatchersJob;
        private readonly UpdateWatchersJob _updateWatchersJob;
        private readonly UpdateOrdersJob _updateOrdersJob;
        private readonly ILogger<MainJob> _logger;
        public MainJob(
            UpdateCurrenciesJob updateCurrenciesJob,
            UpdateLinesJob updateLinesJob,
            UpdateDefaultWatchersJob updateDefaultWatchersJob,
            UpdateWatchersJob updateWatchersJob,
            UpdateOrdersJob updateOrdersJob,
            ILogger<MainJob> logger)
        {
            _updateCurrenciesJob = updateCurrenciesJob;
            _updateLinesJob = updateLinesJob;
            _updateDefaultWatchersJob = updateDefaultWatchersJob;
            _updateWatchersJob = updateWatchersJob;
            _updateOrdersJob = updateOrdersJob;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Run
            await _updateCurrenciesJob.Run();
            await _updateLinesJob.Run();
            await _updateDefaultWatchersJob.Run();
            await _updateWatchersJob.Run();
            await _updateOrdersJob.Run();

            // Stpo watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation(new
            {
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });
        }
    }
}