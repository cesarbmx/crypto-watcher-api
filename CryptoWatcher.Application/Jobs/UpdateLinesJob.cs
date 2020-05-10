using System;
using System.Threading.Tasks;
using Hangfire;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Services;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Jobs
{
    public class UpdateLinesJob
    {
        private readonly LineService _lineService;
        private readonly ILogger<UpdateLinesJob> _logger;

        public UpdateLinesJob(
            LineService lineService,
            ILogger<UpdateLinesJob> logger)
        {
            _lineService = lineService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                await _lineService.UpdateLines();
            }
            catch (Exception ex)
            {
                // Log into Splunk 
                _logger.LogSplunkInformation("UpdateLines", new
                {
                    Failed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }        
    }
}