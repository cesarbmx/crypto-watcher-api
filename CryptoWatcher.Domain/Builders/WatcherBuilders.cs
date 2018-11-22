using System;
using System.Linq;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilders
    {
        public static decimal BuildHype(decimal value, decimal[] values)
        {
            // Take the minimum value
            var min = Math.Abs(values.Min());

            // Add the minimum value to all values so that there are no negatives
            value += min;
            for (var i = 0; i < values.Length; i++)
            {
                values[i] += min;
            }

            // Calculate the verage
            var average = values.Average();

            // Sum up the average
            value += average;

            // Return
            return value;
        }
        public static bool BuildWatcherStatus(decimal setting, decimal value)
        {
            return setting >= value;
        }
    }
}
