using System;
using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;

namespace CesarBmx.CryptoWatcher.Application.Jobs
{
    public class RemoveObsoleteLinesJob
    {
        private readonly LineService _lineService;
        private readonly ILogger<RemoveObsoleteLinesJob> _logger;
        private readonly Tracer _tracer;

        public RemoveObsoleteLinesJob(
            LineService lineService,
            ILogger<RemoveObsoleteLinesJob> logger,
            Tracer tracer)
        {
            _lineService = lineService;
            _logger = logger;
            _tracer = tracer;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start span
                using var span = _tracer.StartActiveSpan(nameof(RemoveObsoleteLinesJob));

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