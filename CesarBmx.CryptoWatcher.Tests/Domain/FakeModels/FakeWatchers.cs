using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Models;


namespace CesarBmx.CryptoWatcher.Tests.Domain.FakeModels
{
    public static class FakeWatchers
    {
        public static List<Watcher> GetWatchersBuyingAndSelling()
        {
            var watchers = new List<Watcher>()
            {
                new Watcher("CesarBmx", "BTC", "Master.PRICE", 3000m,3100m,null,100, 3000m, 3000m,3000m, true, DateTime.UtcNow.StripSeconds()),
                new Watcher("CesarBmx", "BTC", "Master.PRICE", 3000m,2000m,2900,100, 3000m, 3000m,3000m, true, DateTime.UtcNow.StripSeconds()).SetAsBought()

            };
            return watchers;
        }
    }
}
