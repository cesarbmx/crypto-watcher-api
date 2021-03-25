using System;
using System.Linq.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Types;

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
            return x => x.UserId == "Master";
        }
        public static Expression<Func<Watcher, bool>> NonDefaultWatcher()
        {
            return x => x.UserId != "Master";
        }
        public static Expression<Func<Watcher, bool>> DefaultWatcher(string currencyId, string indicatorId)
        {
            return x => x.UserId == "Master" &&
                        (string.IsNullOrEmpty(currencyId) || x.CurrencyId == currencyId) &&
                        (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId);
        }
        public static Func<Watcher, bool> WatcherNotSet()
        {
            return x => !x.Buy.HasValue && !x.Sell.HasValue;
        }
        public static Func<Watcher, bool> WatcherWillingToBuy()
        {
            return x => x.Buy < x.Value && !x.EntryPrice.HasValue && !x.ExitPrice.HasValue;
        }
        public static Func<Watcher, bool> WatcherWillingToSell()
        {
            return x => x.Sell > x.Value && !x.ExitPrice.HasValue && x.EntryPrice.HasValue;
        }
        public static Func<Watcher, bool> WatcherLiquidated()
        {
            return x => x.EntryPrice.HasValue && x.ExitPrice.HasValue;
        }
        public static Expression<Func<Watcher, bool>> WatcherWillingToBuyOrSell()
        {
            return x => x.Value <= x.Buy && !x.EntryPrice.HasValue && !x.ExitPrice.HasValue || x.Value >= x.Sell && !x.ExitPrice.HasValue && x.EntryPrice.HasValue;
        }
        public static Func<Watcher, bool> BuyLimitIsHigherThanWatcherValue(decimal buy)
        {
            return x => buy != x.Buy && buy > x.Value;
        }
        public static Func<Watcher, bool> SellLimitIsLowerThanValue(decimal sell)
        {
            return x => sell != x.Sell && sell < x.Value;
        }
        public static Func<Watcher, bool> WatcherAlreadyBought(decimal buy)
        {
            return x => buy  != x.Buy && x.Status == WatcherStatus.BUYING;
        }
        public static Func<Watcher, bool> WatcherAlreadySold(decimal sell)
        {
            return x => sell != x.Sell && x.Status == WatcherStatus.SELLING;
        }
        public static Func<Watcher, bool> WatcherAlreadyLiquidated()
        {
            return x => x.Status == WatcherStatus.LIQUIDATED;
        }
    }
}
