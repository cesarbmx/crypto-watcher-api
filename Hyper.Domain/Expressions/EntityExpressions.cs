using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class EntityExpressions
    {
        public static Expression<Func<Entity, bool>> HasKey(string id)
        {
            return x => x.Id == id;
        }
    }
}
