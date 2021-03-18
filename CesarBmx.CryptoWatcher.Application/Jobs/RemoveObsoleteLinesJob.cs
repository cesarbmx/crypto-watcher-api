using System;
using System.Threading.Tasks;
using CesarBmx.Shared.Logging.Extensions;
using CesarBmx.CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace CesarBmx.CryptoWatcher.Application.Jobs
{
    public class RemoveObsoleteLinesJob
    {
        private readonly LineService _lineService;
        private readonly ILogger<RemoveObsoleteLinesJob> _logger;

        public RemoveObsoleteLinesJob( LineService lineService, ILogger<RemoveObsoleteLinesJob> logger)
        {
            _lineService = lineService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                await _lineService.RemoveObsoleteLines();
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogSplunkInformation(nameof(_lineService.RemoveObsoleteLines), new
                {
                    Failed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}