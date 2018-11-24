using System;
using System.Threading.Tasks;
using Hangfire;
using CryptoWatcher.Domain.Models;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Api.Jobs
{
    public class UpdateWatchersJob
    {
        readonly ILogger<UpdateWatchersJob> _logger;


        public UpdateWatchersJob(
            ILogger<UpdateWatchersJob> logger)
        {
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Execute()
        {
            try
            {
                // Log into Splunk
                _logger.LogInformation($"Event={nameof(LoggingEvents.WatchersHaveBeenSet)}");

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
               // Log into Splunk 
                _logger.LogError($"Event={nameof(LoggingEvents.UpdatingWatchersHasFailed)}, Exception={ex.Message}");
            }
        }
    }
}