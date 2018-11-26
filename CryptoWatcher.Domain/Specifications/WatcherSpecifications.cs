using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Specifications
{
    public class WatcherReadyToBuySpecification : Specification<Watcher>
    {
        public override Expression<Func<Watcher, bool>> ToExpression()
        {
            return x => //x.WatcherEnabled && // it is enabled
                    x.OperationType == OperationType.Sell;// && // it is willing to sell
                //x.OrderId != null; // it has no order created yet
        }
    }
}
