using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Specifications
{
    public class WatcherReadyToBuySpecification : Specification<Watcher>
    {
        public override Expression<Func<Watcher, bool>> ToExpression()
        {
            return x => //x.WatcherEnabled && // It is enabled
                    x.WatcherStatus == WatcherStatus.Sell;// It is willing to sell
        }
    }
}
