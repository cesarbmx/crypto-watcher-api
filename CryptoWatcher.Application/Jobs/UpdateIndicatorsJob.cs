using System;
using System.Threading.Tasks;
using Hangfire;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Services;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Jobs
{
    public class UpdateIndicatorsJob
    {
        private readonly IndicatorService _indicatorService;
        private readonly ILogger<UpdateIndicatorsJob> _logger;

        public UpdateIndicatorsJob(
            IndicatorService indicatorService,
            ILogger<UpdateIndicatorsJob> logger)
        {
            _indicatorService = indicatorService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                await _indicatorService.UpdateIndicators();
            }
            catch (Exception ex)
            {
                // Log into Splunk 
                _logger.LogSplunkInformation("UpdateIndicators", new
                {
                    JobFailed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}