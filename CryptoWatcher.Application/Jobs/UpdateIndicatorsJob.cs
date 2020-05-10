using System;
using System.Threading.Tasks;
using Hangfire;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Services;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Jobs
{
    public class UpdateIndicatorDependenciesJob
    {
        private readonly IndicatorService _indicatorService;
        private readonly ILogger<UpdateIndicatorDependenciesJob> _logger;

        public UpdateIndicatorDependenciesJob(
            IndicatorService indicatorService,
            ILogger<UpdateIndicatorDependenciesJob> logger)
        {
            _indicatorService = indicatorService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                await _indicatorService.UpdateIndicatorDependencies();
            }
            catch (Exception ex)
            {
                // Log into Splunk 
                _logger.LogSplunkInformation("UpdateIndicatorDependencies", new
                {
                    Failed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}