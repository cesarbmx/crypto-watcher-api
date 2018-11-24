using System;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Shared.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogSplunkInformation<T>(this ILogger<T> logger, string eventId)
        {
            logger.LogInformation($"Event={eventId}");
        }
        public static void LogSplunkError<T>(this ILogger<T> logger, string eventId, string message)
        {
            logger.LogError($"Event={eventId}, Exception={message}");
        }
        public static void LogSplunkError<T>(this ILogger<T> logger, string eventId, Exception ex)
        {
            logger.LogError(ex, $"Event={eventId}");
        }
    }
}
