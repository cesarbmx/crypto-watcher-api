using System;
using System.Threading.Tasks;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Extensions;
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
                _logger.LogSplunkInformation(nameof(LoggingEvents.WatchersHaveBeenSet));

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
               // Log into Splunk 
                _logger.LogSplunkError(nameof(LoggingEvents.UpdatingWatchersHasFailed), ex.Message);
            }
        }
    }
}