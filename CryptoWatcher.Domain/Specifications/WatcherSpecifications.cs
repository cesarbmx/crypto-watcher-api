using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Specifications
{
    public class WatcherSpecification : Specification<Watcher>
    {
        private readonly string _id;

        public WatcherSpecification(string id)
        {
            _id = id;
        }

        public override Expression<Func<Watcher, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
    public class WatcherReadyToBuySpecification : Specification<Watcher>
    {
        public override Expression<Func<Watcher, bool>> ToExpression()
        {
            return x => x.Status == WatcherStatus.Sell;
        }
    }
    public class WatcherFilterSpecification : Specification<Watcher>
    {
        private readonly string _userId;
        private readonly string _id;

        public WatcherFilterSpecification(string userId, string id)
        {
            _userId = userId;
            _id = id;
        }

        public override Expression<Func<Watcher, bool>> ToExpression()
        {
            return x =>
                    (string.IsNullOrEmpty(_userId) || x.UserId == _userId) &&
                    (string.IsNullOrEmpty(_id) || x.Id == _id);
        }
    }
}
