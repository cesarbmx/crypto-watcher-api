using System;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Shared.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogSplunkRequest<T>(this ILogger logger, T payload)
        {
            logger.LogSplunkInformation(typeof(T).Name, payload);
        }
        public static void LogSplunkJob<T>(this ILogger<T> logger, object payload)
        {
            logger.LogSplunkInformation(typeof(T).Name, payload);
        }
        public static void LogSplunkInformation<T>(this ILogger logger, string eventId, T payload)
        {
            var dictionary = payload.AsDictionary();
            var keyValues = dictionary.AsSplunkKeyValueString();
            if (keyValues.Length > 0) keyValues = ", " + keyValues;

            logger.LogInformation($"Event={eventId}" + keyValues);
        }
        public static void LogSplunkError<T>(this ILogger logger, T payload, Exception ex)
        {
            logger.LogError(typeof(T).Name, payload, ex);
        }
        public static void LogSplunkError<T>(this ILogger<T> logger, Exception ex)
        {
            logger.LogError(ex, $"Event={typeof(T).Name}");
        }
    }
}
