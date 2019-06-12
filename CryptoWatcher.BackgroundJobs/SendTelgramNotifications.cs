using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace CryptoWatcher.BackgroundJobs
{
    public class SendTelgramNotifications
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<SendWhatsappNotificationsJob> _logger;
        private readonly IConfiguration _configuration;

        public SendTelgramNotifications(
            MainDbContext mainDbContext,
            ILogger<SendWhatsappNotificationsJob> logger,
            IConfiguration configuration)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
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
                var pendingNotifications = await _mainDbContext.Notifications.Where(NotificationExpression.PendingNotification()).ToListAsync();

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
                            pendingNotification.MarkAsSent();
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
                    await _mainDbContext.SaveChangesAsync();

                    // Stop watch
                    stopwatch.Stop();

                    // Log into Splunk
                    _logger.LogSplunkJob(new
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
                _logger.LogSplunkJob(new
                {
                    JobFailed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}