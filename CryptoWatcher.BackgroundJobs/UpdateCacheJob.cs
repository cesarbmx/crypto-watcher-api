using System.Threading.Tasks;
using Hangfire;


namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateCacheJob
    {
        private readonly UpdateCurrenciesJob _updateCurrenciesJob;
        private readonly UpdateLinesJob _updateLinesJob;
        private readonly UpdateDefaultWatchersJob _updateDefaultWatchersJob;
        public UpdateCacheJob(
            UpdateCurrenciesJob updateCurrenciesJob,
            UpdateLinesJob updateLinesJob,
            UpdateDefaultWatchersJob updateDefaultWatchersJob)
        {
            _updateCurrenciesJob = updateCurrenciesJob;
            _updateLinesJob = updateLinesJob;
            _updateDefaultWatchersJob = updateDefaultWatchersJob;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            await _updateCurrenciesJob.Run();
            await _updateLinesJob.Run();
            await _updateDefaultWatchersJob.Run();
        }
    }
}