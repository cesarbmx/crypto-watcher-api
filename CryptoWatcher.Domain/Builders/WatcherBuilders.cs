using System.Linq;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilders
    {
        public static void BuildHypes(decimal[] values)
        {
            // If there are negatives, we move all values to the right
            // In this way we deal with differences
            var min = values.Min();
            if (min < 0)
            {
                for (var i = 0; i < values.Length; i++)
                {
                    values[i] = values[i] + min;
                }
            }

            // Calculate the average of these differences
            var average = values.Average();

            // Eliminate those values below the average
            for (var i = 0; i < values.Length; i++)
            {
                values[i] -= average;
                values[i] = values[i] < 0 ? 0 : values[i];
            }
        }
        public static bool BuildWatcherStatus(decimal setting, decimal value)
        {
            // Return
            return value >= setting;
        }
    }
}
