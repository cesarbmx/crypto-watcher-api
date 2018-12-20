using System;
using System.Linq.Expressions;
using CryptoWatcher.Shared.Models;

namespace CryptoWatcher.Shared.Expressions
{
    public static class EntityExpression
    {
        public static Expression<Func<IEntity, bool>> Entity(string id)
        {
            return x =>  x.Id == id;
        }
    }
}
