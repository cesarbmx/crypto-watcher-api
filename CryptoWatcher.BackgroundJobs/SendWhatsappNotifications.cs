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

namespace CryptoWatcher.BackgroundJobs
{
    public class SendWhatsappNotificationsJob
    {
        private readonly ILogger<MonitorWatchersJob> _logger;
        private readonly NotificationService _notificationService;


        public SendWhatsappNotificationsJob(
            ILogger<MonitorWatchersJob> logger,
            NotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {

                // Get pending notifications
                var pendingNotifications = await _notificationService.GetPendingNotifications();

                // If there are pending notifications
                if (pendingNotifications.Count > 0)
                {
                    // Connect
                    TwilioClient.Init(
                        Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"),
                        Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")
                    );
                    
                    // For each notification
                    foreach (var pendingNotification in pendingNotifications)
                    {
                        try
                        {
                            // Send whatsapp
                            MessageResource.Create(
                                from: new PhoneNumber("whatsapp:" + pendingNotification.PhoneNumber),
                                to: new PhoneNumber("whatsapp:" + "+34666666666"),
                                body: pendingNotification.Message
                            );
                            pendingNotification.SendWhatsapp();
                            _logger.LogSplunkInformation(nameof(LoggingEvents.WatchappsSent), pendingNotification);
                        }
                        catch (Exception ex)
                        {
                            // Log into Splunk
                            _logger.LogSplunkError(nameof(LoggingEvents.SendingWatchappFailed), pendingNotification, ex);
                        }
                    }
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogSplunkError(nameof(LoggingEvents.ConnectingToTwilioFailed), ex);
            }
        }
    }
}