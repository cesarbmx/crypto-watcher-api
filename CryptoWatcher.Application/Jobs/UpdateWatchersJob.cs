using System;
using System.Threading.Tasks;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Jobs
{
    public class UpdateWatchersJob
    {
        private readonly WatcherService _watcherService;
        private readonly ILogger<UpdateWatchersJob> _logger;
        public UpdateWatchersJob(
            WatcherService watcherService,
            ILogger<UpdateWatchersJob> logger)
        {
            _watcherService = watcherService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                await _watcherService.UpdateWatchers();
            }
            catch (Exception ex)
            {
                // Log into Splunk 
                _logger.LogSplunkInformation("UpdateWatchers", new
                {
                    JobFailed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}