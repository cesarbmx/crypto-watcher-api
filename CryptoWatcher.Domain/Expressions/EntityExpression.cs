using System;
using System.Linq.Expressions;
using CryptoWatcher.Shared.Domain;

namespace CryptoWatcher.Domain.Expressions
{
    public static class EntityExpression
    {
        public static Expression<Func<IEntity, bool>> Entity(string id)
        {
            return x =>  x.Id == id;
        }
    }
}
