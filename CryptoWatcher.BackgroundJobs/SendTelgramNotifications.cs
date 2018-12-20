﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Contexts;
using CryptoWatcher.Shared.Extensions;
using CryptoWatcher.Shared.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace CryptoWatcher.BackgroundJobs
{
    public class SendTelgramNotifications
    {
        private readonly IContext _context;
        private readonly ILogger<SendWhatsappNotificationsJob> _logger;
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IConfiguration _configuration;

        public SendTelgramNotifications(
            IContext context,
            ILogger<SendWhatsappNotificationsJob> logger,
            IRepository<Notification> notificationRepository,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _notificationRepository = notificationRepository;
            _configuration = configuration;
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
                    var apiToken = _configuration["AppSettings:TelegramApiToken"];
                    var bot = new TelegramBotClient(apiToken);

                    // For each notification
                    var count = 0;
                    var failedCount = 0;
                    foreach (var pendingNotification in pendingNotifications)
                    {
                        try
                        {
                            // Send whatsapp
                            await bot.SendTextMessageAsync("@crypto_watcher_official", pendingNotification.Message);
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