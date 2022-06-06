using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Application.Settings;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace CesarBmx.CryptoWatcher.Application.Services
{
    public class NotificationService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            MainDbContext mainDbContext,
            IMapper mapper,
            AppSettings appSettings,
            ILogger<NotificationService> logger)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _appSettings = appSettings;
            _logger = logger;
        }

        public async Task<List<Responses.Notification>> GetUserNotifications(string userId)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get user
            var notifications = await _mainDbContext.Notifications
                .Where(x => x.UserId == userId).ToListAsync();

            // Response
            var response = _mapper.Map<List<Responses.Notification>>(notifications);

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

        public async Task<List<Notification>> AddNotifications(List<Order> orders)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Now
            var now = DateTime.UtcNow.StripSeconds();

            // Create notifications
            var notifications = new List<Notification>();

            // Orders pending to notify
            var ordersPendingToNotify = orders.Where(OrderExpression.PendingToNotify()).ToList();

            // For each order pending to notify
            foreach (var order in ordersPendingToNotify)
            {
                // Get user
                var user = await _mainDbContext.Users.FindAsync(order.UserId);

                // Create message
                var message = NotificationBuilder.BuildMessage(
                    OrderMessage.OrderNotification, 
                    order.CurrencyId, 
                    order.OrderType, 
                    order.Price);

                // Create notification
                var notification = new Notification(user.UserId, user.PhoneNumber, message, now);

                // Add notification
                notifications.Add(notification);

                // Mark order as notified
                order.MarkAsNotified();

                // Update order
                _mainDbContext.Orders.Update(order);
            }

            // Add notifications
            await _mainDbContext.Notifications.AddRangeAsync(notifications);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log
            _logger.LogInformation("{@Event}, {@Id}, {@Count}, {@ExecutionTime}", "NotificationsAdded", Guid.NewGuid(), notifications.Count, stopwatch.Elapsed.TotalSeconds);

            // Return
            return notifications;
        }
        public async Task SendTelegramNotifications()
        {
            // Get pending notifications
            var pendingNotifications = await _mainDbContext.Notifications
                .Where(NotificationExpression.PendingNotification())
                .ToListAsync();

            await SendTelegramNotifications(pendingNotifications);
        }
        public async Task SendTelegramNotifications(List<Notification> notifications)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get pending notifications
            //var notifications = await _mainDbContext.Notifications.Where(NotificationExpression.PendingNotification()).ToListAsync();

            // Connect
            var apiToken = _appSettings.TelegramApiToken;
            var bot = new TelegramBotClient(apiToken);

            // For each notification
            var count = 0;
            var failedCount = 0;
            foreach (var notification in notifications)
            {
                try
                {
                    // Send telegram
                    await bot.SendTextMessageAsync("@crypto_watcher_official", notification.Message);

                    // Mark notification as sent
                    notification.MarkAsSent();

                    // Update notification
                    _mainDbContext.Notifications.Update(notification);

                    // Save
                    await _mainDbContext.SaveChangesAsync();

                    // Count
                    count++;
                }
                catch (Exception ex)
                {
                    // Log
                    _logger.LogError(ex, ex.Message);
                    failedCount++;
                }
            }

            // Stop watch
            stopwatch.Stop();

            // Log
            _logger.LogInformation("{@Event}, {@Id}, {@Count}, {@FailedCount}, {@ExecutionTime}", "TelegramNotificationsSent", Guid.NewGuid(), count, failedCount, stopwatch.Elapsed.TotalSeconds);
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
                        // Log
                        _logger.LogError(ex, ex.Message);
                        failedCount++;
                    }
                }

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log
                _logger.LogInformation("{@Event}, {@Id}, {@Count}, {@FailedCount}, {@ExecutionTime}", "WhatsappNotificationsSent", Guid.NewGuid(), count, failedCount, stopwatch.Elapsed.TotalSeconds);
            }
        }
    }
}
