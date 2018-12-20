using System;

namespace CryptoWatcher.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert datetime to a friendly human readable format
        /// </summary>
        /// <param name="dateTime">Datetime to convert</param>
        /// <returns>A string</returns>
        public static string DaysHoursMinutesAndSecondsSinceDate(this DateTime dateTime)
        {
            var span = (DateTime.Now - dateTime);
            return $"{(span.Days > 1 ? span.Days + " days, " : span.Days == 1 ? "1 day, " : "")}" +
                   $"{(span.Hours > 1 ? span.Hours + " hours, " : span.Hours == 1 ? "1 hour, " : "")}" +
                   $"{(span.Minutes > 1 ? span.Minutes + " minutes and " : span.Minutes == 1 ? "1 minute and " : "")}" +
                   $"{(span.Seconds > 1 ? span.Seconds + " seconds " : span.Seconds == 1 ? "1 second " : "0 second ")}ago";
        }
    }
}
