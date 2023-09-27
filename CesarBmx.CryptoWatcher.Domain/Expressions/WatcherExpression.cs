using System;
using System.Linq.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Domain.Expressions
{
    public static class WatcherExpression
    {
        public static Expression<Func<Watcher, bool>> Unique(string userId, string currencyId, string indicatorUserId, string indicatorId)
        {
            return x =>
                x.UserId == userId &&
                x.CurrencyId == currencyId &&
                x.IndicatorId == indicatorId;
        }
        public static Expression<Func<Watcher, bool>> Filter(string userId = null, string currencyId = null, string indicatorId = null)
        {
            return x => (string.IsNullOrEmpty(userId) || x.UserId == userId) &&
                        (string.IsNullOrEmpty(currencyId) || x.CurrencyId == currencyId) &&
                        (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId);
        }
        public static Expression<Func<Watcher, bool>> DefaultWatcher()
        {
            return x => x.UserId == "master";
        }
        public static Expression<Func<Watcher, bool>> NonDefaultWatcher()
        {
            return x => x.UserId != "master";
        }
        public static Expression<Func<Watcher, bool>> DefaultWatcher(string currencyId, string indicatorId)
        {
            return x => x.UserId == "master" &&
                        (string.IsNullOrEmpty(currencyId) || x.CurrencyId == currencyId) &&
                        (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId);
        }
        public static Func<Watcher, bool> WatcherNotSet()
        {
            return x => !x.Buy.HasValue;
        }
        public static Expression<Func<Watcher, bool>> WatcherSet()
        {
            return x => x.Status != Types.WatcherStatus.NOT_SET;
        }
        public static Func<Watcher, bool> WatcherBuying()
        {
            return x => x.Status == Types.WatcherStatus.BUYING;
        }
        public static Func<Watcher, bool> WatcherSelling()
        {
            return x => x.Status == Types.WatcherStatus.SELLING;
        }
        public static Func<Watcher, bool> WatcherHoldingOrSelling()
        {
            return x => x.Status == Types.WatcherStatus.HOLDING || x.Status == Types.WatcherStatus.SELLING;
        }
        public static Func<Watcher, bool> WatcherBought()
        {
            return x => x.Status == Types.WatcherStatus.HOLDING || x.Status == Types.WatcherStatus.SELLING || x.Status == Types.WatcherStatus.SOLD;
        }
        public static Func<Watcher, bool> WatcherHolding()
        {
            return x => x.Status == Types.WatcherStatus.HOLDING;
        }
        public static Func<Watcher, bool> WatcherSold()
        {
            return x => x.Status == Types.WatcherStatus.SOLD;
        }
        public static Func<Watcher, bool> BuyLimitHigherThanWatcherValue(decimal buy)
        {
            return x => buy > x.Value;
        }
        public static Func<Watcher, bool> SellLimitLowerThanWatcherValue(decimal? sell)
        {
            return x => sell < x.Value;
        }
    }
}
