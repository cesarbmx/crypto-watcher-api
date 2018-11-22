


using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilders
    {
        public static decimal BuildHype(decimal value, decimal[] values)
        {
            var average = MathHelper.Average(values);
            value += average;
            return value;
        }
        public static bool BuildWatcherStatus(decimal setting, decimal value)
        {
            return setting >= value;
        }
    }
}
