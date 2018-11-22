using System;
using System.Linq;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilders
    {
        public static decimal BuildHype(decimal value, decimal[] values)
        {
            // Take the minimum value and ensure it positive
            var positiveMinum = Math.Abs(values.Min());

            // Add the minimum value to all values so that there are no negatives
            var positiveValue = value + positiveMinum;
            var positiveValues = new decimal[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                positiveValues[i] = values[i] + positiveMinum;
            }

            // Calculate the average (which will obviously be positive)
            var positiveAverage = positiveValues.Average();

            // Sum up the average
            positiveValue += positiveAverage;

            // Return
            return positiveValue;
        }
        public static bool BuildWatcherStatus(decimal setting, decimal value)
        {
            return setting >= value;
        }
    }
}
