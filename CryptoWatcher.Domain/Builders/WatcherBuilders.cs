using System.Linq;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilders
    {
        public static decimal BuildHype(decimal value, decimal[] values)
        {
            // Move negatives to zero so that we only look at positive values (price increases)
            var positiveValue = value < 0 ? 0 : value;
            var positiveValues = new decimal[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                positiveValues[i] = values[i] < 0 ? 0 : values[i];
            }

            // Substract the average to see what values are really hyping up
            var average = positiveValues.Average();
            positiveValue -= average;

            // Make the value zero if it does not surpass the average
            positiveValue = positiveValue < 0 ? 0 : positiveValue;

            // Return
            return positiveValue;
        }
        public static bool BuildWatcherStatus(decimal setting, decimal value)
        {
            // Return
            return value >= setting;
        }
    }
}
