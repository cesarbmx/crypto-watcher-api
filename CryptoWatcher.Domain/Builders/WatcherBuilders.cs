using System.Linq;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilders
    {
        public static void BuildHypes(decimal[] values)
        {
            // If there are negatives, we move all the values to the right so we only deal with positives
            var min = values.Min();
            if (min < 0)
            {
                for (var i = 0; i < values.Length; i++)
                {
                    values[i] = values[i] + min;
                }
            }

            // We calculate the average
            var average = values.Average();

            // We want the values above the average
            for (var i = 0; i < values.Length; i++)
            {
                values[i] -= average;
                // We set to zero the values below the average
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
