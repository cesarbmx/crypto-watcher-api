using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace CesarBmx.CryptoWatcher.Application.Jobs
{
    public class RemoveObsoleteLinesJob
    {
        private readonly LineService _lineService;
        private readonly ILogger<RemoveObsoleteLinesJob> _logger;
        private readonly ActivitySource _activitySource;

        public RemoveObsoleteLinesJob(
            LineService lineService,
            ILogger<RemoveObsoleteLinesJob> logger,
            ActivitySource activitySource)
        {
            _lineService = lineService;
            _logger = logger;
            _activitySource = activitySource;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start span
                using var span = _activitySource.StartActivity(nameof(RemoveObsoleteLinesJob));

                // Remove obsolete lines
                await _lineService.RemoveObsoleteLines();
            }
            catch (Exception ex)
            {
                // Log
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}