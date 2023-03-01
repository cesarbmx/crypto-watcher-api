using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CesarBmx.Notification.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace CesarBmx.Notification.Application.Jobs
{
    public class SendWhatsappNotificationsJob
    {
        private readonly NotificationService _notificationService;
        private readonly ILogger<SendWhatsappNotificationsJob> _logger;
        private readonly ActivitySource _activitySource;

        public SendWhatsappNotificationsJob(
            NotificationService notificationService,
            ILogger<SendWhatsappNotificationsJob> logger,
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
                using var span = _activitySource.StartActivity(nameof(SendWhatsappNotificationsJob));

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