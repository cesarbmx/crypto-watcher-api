using System;
using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;

namespace CesarBmx.CryptoWatcher.Application.Jobs
{
    public class SendWhatsappNotificationsJob
    {
        private readonly NotificationService _notificationService;
        private readonly ILogger<SendWhatsappNotificationsJob> _logger;
        private readonly Tracer _tracer;

        public SendWhatsappNotificationsJob(
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

                // Send whatsapp notifications
                await _notificationService.SendWhatsappNotifications();
            }
            catch (Exception ex)
            {
                // Log
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}