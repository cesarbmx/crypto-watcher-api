using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class WatcherExpression
    {
        public static Expression<Func<Watcher, bool>> Watcher(string userId, string targetId, string indicatorId)
        {
            return x =>
                x.UserId == userId &&
                x.TargetId == targetId &&
                x.IndicatorId == indicatorId;
        }
        public static Expression<Func<Watcher, bool>> WatcherFilter(string userId = null, string currencyId = null, string indicatorId = null)
        {
            return x => (string.IsNullOrEmpty(userId) || x.UserId == userId) &&
                        (string.IsNullOrEmpty(currencyId) || x.TargetId == currencyId) &&
                        (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId);
        }
        public static Expression<Func<Watcher, bool>> DefaultWatcher()
        {
            return x => x.UserId == "master";
        }
        public static Expression<Func<Watcher, bool>> DefaultWatcher(string targetId, string indicatorId)
        {
            return x => x.UserId == "master" &&
                        (string.IsNullOrEmpty(targetId) || x.TargetId == targetId) &&
                        (string.IsNullOrEmpty(indicatorId) || x.IndicatorId == indicatorId);
        }
        public static Expression<Func<Watcher, bool>> WatcherWillingToBuyOrSell()
        {
            return x => x.Status != WatcherStatus.Hold;
        }
    }
}
