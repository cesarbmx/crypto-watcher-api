using System;
using System.Threading.Tasks;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CryptoWatcher.Api.Jobs
{
    public class SendWhatsappNotificationsJob
    {
        readonly ILogger<UpdateWatchersJob> _logger;
        readonly NotificationService _notificationService;


        public SendWhatsappNotificationsJob(
            ILogger<UpdateWatchersJob> logger,
            NotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Execute()
        {
            try
            {
                TwilioClient.Init(
                    Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"),
                    Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")
                );
                // Get pending notifications
                var pendingNotifications = await _notificationService.GetPendingNotifications();

                foreach (var pendingNotification in pendingNotifications)
                {
                    MessageResource.Create(
                        from: new PhoneNumber("whatsapp:" + pendingNotification.PhoneNumber),
                        to: new PhoneNumber("whatsapp:" + "+34666666666"),
                        body: pendingNotification.Message
                    );
                }

                // Log into Splunk
                _logger.LogSplunkInformation(nameof(LoggingEvents.WatchappsHaveBeenSent));

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogSplunkError(nameof(LoggingEvents.SendingWatchappsHasFailed), ex.Message);
            }
        }
    }
}