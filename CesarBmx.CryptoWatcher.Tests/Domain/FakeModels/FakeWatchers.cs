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
        public static List<Watcher> GetWatchersHolding()
        {
            var watchers = new List<Watcher>()
            {
                new Watcher("CesarBmx", "BTC", "Master.PRICE", 3000m,2900m,null,100, 3000m, 3000m,3000m, true, DateTime.UtcNow.StripSeconds()).SetAsBought(),
                new Watcher("CesarBmx", "BTC", "Master.PRICE", 3000m,2800m,null,100, 3000m, 3000m,3000m, true, DateTime.UtcNow.StripSeconds()).SetAsBought()

            };
            return watchers;
        }
        public static List<Watcher> GetWatchersLiquidated()
        {
            var watchers = new List<Watcher>()
            {
                new Watcher("CesarBmx", "BTC", "Master.PRICE", 6000m,2900m,3500,100, 3000m, 3000m,3000m, true, DateTime.UtcNow.StripSeconds()).SetAsBought().SetAsSold(),
                new Watcher("CesarBmx", "BTC", "Master.PRICE", 2000m,2800m,3500,100, 3000m, 3000m,3000m, true, DateTime.UtcNow.StripSeconds()).SetAsBought().SetAsSold()

            };
            return watchers;
        }
    }
}
