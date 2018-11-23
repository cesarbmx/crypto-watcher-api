using System.Linq;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilders
    {
        public static decimal BuildHype(decimal value, decimal[] values)
        {
            // Take the minimum value
            var minimum = values.Min() * -1;

            // Move all values to the right so that there are no negatives
            var positiveValue = value + minimum;
            var positiveValues = new decimal[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                positiveValues[i] = values[i] + minimum;
            }

            // Substract the average to see what values are hyping up
            var average = positiveValues.Average();
            positiveValue -= average;
            if (positiveValue < 0) positiveValue = 0;

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
