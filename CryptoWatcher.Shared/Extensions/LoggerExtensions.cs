using System;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Shared.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogSplunkInformation<T>(this ILogger<T> logger, object payload)
        {
            logger.LogSplunkInformation(typeof(T).Name, payload);
        }
        public static void LogSplunkInformation<T>(this ILogger<T> logger, string eventId, object payload)
        {
            var dictionary = payload.AsDictionary();
            var keyValues = dictionary.AsSplunkKeyValueString();
            if (keyValues.Length > 0) keyValues = ", " + keyValues;

            logger.LogInformation($"Event={eventId}" + keyValues);
        }
        public static void LogSplunkError<T>(this ILogger<T> logger, Exception ex)
        {
            logger.LogError(ex, $"Event={typeof(T).Name}");
        }
        public static void LogSplunkError<T>(this ILogger<T> logger, object payload, Exception ex)
        {
            logger.LogError(typeof(T).Name, payload, ex);
        }
        public static void LogSplunkError<T>(this ILogger<T> logger, string eventId, Exception ex)
        {
            logger.LogError(ex, $"Event={eventId}");
        }
    }
}
