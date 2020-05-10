using System;
using System.Threading.Tasks;
using Hangfire;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Services;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Jobs
{
    public class UpdateDefaultWatchersJob
    {
        private readonly WatcherService _watcherService;
        private readonly ILogger<UpdateDefaultWatchersJob> _logger;
        public UpdateDefaultWatchersJob(
            WatcherService watcherService,
            ILogger<UpdateDefaultWatchersJob> logger)
        {
            _watcherService = watcherService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                await _watcherService.UpdateDefaultWatchers();
            }
            catch (Exception ex)
            {
                // Log into Splunk 
                _logger.LogSplunkInformation("UpdateDefaultWatchers", new
                {
                    Failed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}