using System;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CryptoWatcher.BackgroundJobs
{
    public class SendWhatsappNotificationsJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<MonitorWatchersJob> _logger;
        private readonly IRepository<Notification> _notificationRepository;
        
        public SendWhatsappNotificationsJob(
            MainDbContext mainDbContext,
            ILogger<MonitorWatchersJob> logger,
            IRepository<Notification> notificationRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _notificationRepository = notificationRepository;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Get pending notifications
                var pendingNotifications = await _notificationRepository.GetAll(NotificationExpression.PendingNotification());

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
                        }
                        catch (Exception ex)
                        {
                            // Log into Splunk
                            _logger.LogSplunkError(pendingNotification, ex);
                        }
                    }

                    // Save
                    await _mainDbContext.SaveChangesAsync();
                }

                // Log into Splunk
                _logger.LogSplunkInformation();

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}