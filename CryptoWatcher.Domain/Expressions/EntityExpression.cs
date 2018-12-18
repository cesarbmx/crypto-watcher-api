using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class EntityExpression
    {
        public static Expression<Func<Entity, bool>> Entity(string id)
        {
            return x =>  x.Id == id;
        }
    }
}
