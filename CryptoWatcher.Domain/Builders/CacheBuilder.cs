using System.Collections.Generic;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class CacheBuilder
    {
        public static Cache BuildCache<T>(CacheKey key, List<T> value)
        {
            var cache = new Cache();
            cache.Set(key, value);
            return cache;
        }     
    }
}
