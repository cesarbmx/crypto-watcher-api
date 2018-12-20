﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Contexts;
using CryptoWatcher.Shared.Extensions;
using CryptoWatcher.Shared.Repositories;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CryptoWatcher.BackgroundJobs
{
    public class SendWhatsappNotificationsJob
    {
        private readonly IContext _context;
        private readonly ILogger<SendWhatsappNotificationsJob> _logger;
        private readonly IRepository<Notification> _notificationRepository;

        public SendWhatsappNotificationsJob(
            IContext context,
            ILogger<SendWhatsappNotificationsJob> logger,
            IRepository<Notification> notificationRepository)
        {
            _context = context;
            _logger = logger;
            _notificationRepository = notificationRepository;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

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
                    var count = 0;
                    var failedCount = 0;
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
                            count++;
                        }
                        catch (Exception ex)
                        {
                            // Log into Splunk
                            _logger.LogSplunkError(pendingNotification, ex);
                            failedCount++;
                        }
                    }

                    // Save
                    await _context.SaveChangesAsync();

                    // Stop watch
                    stopwatch.Stop();

                    // Log into Splunk
                    _logger.LogSplunkInformation(new
                    {
                        Count = count,
                        FailedCount = failedCount,
                        ExecutionTime = stopwatch.Elapsed.TotalSeconds
                    });

                    // Return
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}