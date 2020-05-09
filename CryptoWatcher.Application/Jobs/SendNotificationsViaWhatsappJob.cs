using System;
using System.Threading.Tasks;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Jobs
{
    public class SendNotificationsViaWhatsappJob
    {
        private readonly NotificationService _notificationService;
        private readonly ILogger<SendNotificationsViaWhatsappJob> _logger;

        public SendNotificationsViaWhatsappJob(
            NotificationService notificationService,
            ILogger<SendNotificationsViaWhatsappJob> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                await _notificationService.SendNotificationsViaWhatsapp();
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogSplunkInformation(new
                {
                    JobFailed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}