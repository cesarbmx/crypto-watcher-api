using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace CryptoWatcher.Application.Services
{
    public class NotificationService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            MainDbContext mainDbContext,
            IMapper mapper,
            IConfiguration configuration,
            ILogger<NotificationService> logger)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Responses.Notification>> GetUserNotifications(string userId)
        {
            // Get user
            var user = await _mainDbContext.Users
                .Include(x=>x.Notifications)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Response
            var response = _mapper.Map<List<Responses.Notification>>(user.Notifications);

            // Return
            return response;
        }

        public async Task<Responses.Notification> GetNotification(Guid notificationId)
        {
            // Get notification
            var notification = await _mainDbContext.Notifications.FindAsync(notificationId);

            // Throw NotFound if the currency does not exist
            if (notification == null) throw new NotFoundException(NotificationMessage.NotificationNotFound);

            // Response
            var response = _mapper.Map<Responses.Notification>(notification);

            // Return
            return response;
        }
        public async Task SendTelegramNotifications()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get pending notifications
            var pendingNotifications = await _mainDbContext.Notifications.Where(NotificationExpression.PendingNotification()).ToListAsync();

            // If there are pending notifications
            if (pendingNotifications.Count == 0) return;

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
                    pendingNotification.MarkAsSent();
                    count++;
                }
                catch (Exception ex)
                {
                    // Log into Splunk
                    _logger.LogSplunkError(ex);
                    failedCount++;
                }
            }

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("SendTelegramNotifications", new
            {
                Count = count,
                FailedCount = failedCount,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });
        }
        public async Task SendWhatsappNotifications()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get pending notifications
            var pendingNotifications = await _mainDbContext.Notifications.Where(NotificationExpression.PendingNotification()).ToListAsync();

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
                        pendingNotification.MarkAsSent();
                        count++;
                    }
                    catch (Exception ex)
                    {
                        // Log into Splunk
                        _logger.LogSplunkError(ex);
                        failedCount++;
                    }
                }

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation("SendWhatsappNotifications", new
                {
                    Count = count,
                    FailedCount = failedCount,
                    ExecutionTime = stopwatch.Elapsed.TotalSeconds
                });
            }
        }
    }
}
