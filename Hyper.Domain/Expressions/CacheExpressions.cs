using System;
using System.Linq.Expressions;
using Hyper.Domain.Models;

namespace Hyper.Domain.Expressions
{
    public static class CacheExpressions
    {
        public static Expression<Func<Cache, bool>> HasKey(string key)
        {
            return x => x.Key == key;
        }
    }
}
