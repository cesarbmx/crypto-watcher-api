using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Models;


namespace CesarBmx.CryptoWatcher.Tests.Domain.FakeModels
{
    public static class FakeWatcher
    {
        public static List<Watcher> GetWatchersNotSet()
        {
            var watchers = new List<Watcher>()
            {
                new Watcher("cesarbmx", "BTC", "master.PRICE", false, DateTime.UtcNow.StripSeconds()),
                new Watcher("cesarbmx", "BTC", "master.PRICE", false, DateTime.UtcNow.StripSeconds())

            };
            return watchers;
        }
        public static List<Watcher> GetWatchersSet()
        {
            var watchers = new List<Watcher>()
            {
               GetWatchersNotSet()[0]
               .Set(
                   buy:2000m, 
                   sell:4000m, 
                   quantity:100)
               .Sync(
                   averageBuyValue:2000m,
                   averageSellValue:4000m,
                   value:3000m, 
                   price:3000m),
               GetWatchersNotSet()[1]
               .Set(
                   buy:2000m,
                   sell:null,
                   quantity:100)
               .Sync(
                   averageBuyValue:2000m, 
                   averageSellValue:4000m, 
                   value:3000m, 
                   price:3000m)
            };
            return watchers;
        }
        public static List<Watcher> GetWatchersBuying()
        {
            var watchers = new List<Watcher>()
            {
                GetWatchersSet()[0]
                .Sync(
                    averageBuyValue:2000m,
                    averageSellValue:4000m,
                    value:2000m,
                    price:2000m),
                GetWatchersSet()[1]
                .Sync(
                    averageBuyValue:2000m,
                    averageSellValue:4000m,
                    value:2000m,
                    price:2000m)
            };
            return watchers;
        }
        public static List<Watcher> GetWatchersSelling()
        {
            var watchers = new List<Watcher>()
            {
                GetWatchersBuying()[0]
                .Sync(
                    averageBuyValue:2000m,
                    averageSellValue:4000m,
                    value:4000m,
                    price:4000m)
            };
            return watchers;
        }
        public static List<Watcher> GetWatchersHolding()
        {
            var watchers = new List<Watcher>()
            {
                GetWatchersBuying()[1]
                .ConfirmBuyOrder(
                    price:4000m,
                    executedAt:DateTime.UtcNow.StripSeconds())
            };
            return watchers;
        }
        public static List<Watcher> GetWatchersSold()
        {
            var watchers = new List<Watcher>()
            {
                GetWatchersSelling()[0]
                .Sync(
                    averageBuyValue:2000m,
                    averageSellValue:4000m,
                    value:4000m,
                    price:4000m)
                .ConfirmOrder(
                    price:4000m,
                    executedAt:DateTime.UtcNow.StripSeconds())
            };
            return watchers;
        }
        public static List<Watcher> GetWatchersBuyingWithDifferentWeights()
        {
            var watchers = new List<Watcher>()
            {
                new Watcher("cesarbmx", "BTC", "master.PRICE", true, DateTime.UtcNow.StripSeconds())
                .Set(
                    buy:30000m,
                    sell:50000m,
                    quantity:100)
                .Sync(
                    averageBuyValue:null,
                    averageSellValue:null,
                    value:30000m,
                    price:30000m),
                new Watcher("cesarbmx", "BTC", "master.PRICE", true, DateTime.UtcNow.StripSeconds())
                .Set(
                    buy:2000m,
                    sell:4000m,
                    quantity:200)
                .Sync(
                    averageBuyValue:null,
                    averageSellValue:null,
                    value:30000m,
                    price:30000m)
            };
            return watchers;
        }
        public static List<Watcher> GetWatchersSellingWithDifferentWeights()
        {
            var watchers = new List<Watcher>()
            {
                new Watcher("cesarbmx", "BTC", "master.PRICE", true, DateTime.UtcNow.StripSeconds())
                .Set(
                    buy:3000m,
                    sell:50000m,
                    quantity:100)
                .Sync(
                    averageBuyValue:null,
                    averageSellValue:null,
                    value:30000m,
                    price:30000m)
                .ConfirmBuyOrder(
                    price:30000m,
                    executedAt: DateTime.UtcNow.StripSeconds())
                 .Sync(
                    averageBuyValue:null,
                    averageSellValue:null,
                    value:30000m,
                    price:30000m),
                new Watcher("cesarbmx", "BTC", "master.PRICE", true, DateTime.UtcNow.StripSeconds())
                .Set(
                    buy:2000m,
                    sell:4000m,
                    quantity:200)
                .Sync(
                    averageBuyValue:null,
                    averageSellValue:null,
                    30000m,
                    30000m)
                 .ConfirmBuyOrder(
                    price:30000m,
                    executedAt: DateTime.UtcNow.StripSeconds())
                 .Sync(
                    averageBuyValue:null,
                    averageSellValue:null,
                    value:30000m,
                    price:30000m),
            };
            return watchers;
        }
    }
}
