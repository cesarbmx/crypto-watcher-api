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
        public static void LogSplunkInformation<T>(this ILogger<T> logger, string eventId, object payload)
        {
            var dictionary = payload.AsDictionary();
            var keyValues = dictionary.AsSplunkKeyValueString();
            if (keyValues.Length > 0) keyValues = ", " + keyValues;

            logger.LogInformation($"Event={eventId}" + keyValues);
        }   
        public static void LogSplunkError<T>(this ILogger<T> logger, string eventId, Exception ex)
        {
            logger.LogError(ex, $"Event={eventId}");
        }
        public static void LogSplunkError<T>(this ILogger<T> logger, string eventId, object payload, Exception ex)
        {
            var dictionary = payload.AsDictionary();
            var keyValues = dictionary.AsSplunkKeyValueString();
            if (keyValues.Length > 0) keyValues = ", " + keyValues;

            logger.LogError(ex, $"Event={eventId}" + keyValues);
        }
    }
}
