using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace CesarBmx.CryptoWatcher.Application.Jobs
{
    public class SendTelgramNotificationsJob
    {
        private readonly NotificationService _notificationService;
        private readonly ILogger<SendTelgramNotificationsJob> _logger;
        private readonly ActivitySource _activitySource;

        public SendTelgramNotificationsJob(
            NotificationService notificationService,
            ILogger<SendTelgramNotificationsJob> logger,
            ActivitySource activitySource)
        {
            _notificationService = notificationService;
            _logger = logger;
            _activitySource = activitySource;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start span
                using var span = _activitySource.StartActivity(nameof(SendTelgramNotificationsJob));

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