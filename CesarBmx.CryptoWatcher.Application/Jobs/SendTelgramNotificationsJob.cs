using System;
using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;

namespace CesarBmx.CryptoWatcher.Application.Jobs
{
    public class SendTelgramNotificationsJob
    {
        private readonly NotificationService _notificationService;
        private readonly ILogger<SendWhatsappNotificationsJob> _logger;
        private readonly Tracer _tracer;

        public SendTelgramNotificationsJob(
            NotificationService notificationService,
            ILogger<SendWhatsappNotificationsJob> logger,
            Tracer tracer)
        {
            _notificationService = notificationService;
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

                // Send telegram notifications
                await _notificationService.SendTelegramNotifications();
            }
            catch (Exception ex)
            {
                // Log
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}