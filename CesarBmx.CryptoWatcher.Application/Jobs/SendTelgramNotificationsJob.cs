using System;
using System.Threading.Tasks;
using CesarBmx.Shared.Logging.Extensions;
using CesarBmx.CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace CesarBmx.CryptoWatcher.Application.Jobs
{
    public class SendTelgramNotificationsJob
    {
        private readonly NotificationService _notificationService;
        private readonly ILogger<SendWhatsappNotificationsJob> _logger;

        public SendTelgramNotificationsJob(
            NotificationService notificationService,
            ILogger<SendWhatsappNotificationsJob> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                await _notificationService.SendTelegramNotifications();
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogSplunkInformation(nameof(_notificationService.SendTelegramNotifications), new
                {
                    Failed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}