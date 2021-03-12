using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class FakeWatcher
    {
        public static Watcher GetFake_master_Bitcoin_Price()
        {
            return new Watcher
            {
                WatcherId = 1,
                UserId = "cesarbmx",
                CurrencyId = "btc",
                IndicatorId = "price",
                CreatorId = "master",
                Value = 5000,
                Buy = 15,
                Sell = 8,
                Quantity = 100,
                AverageBuy = 0,
                AverageSell = 0,
                Price = 5000,
                EntryPrice = 14,
                ExitPrice = 20,
                Status = WatcherStatus.BUY,
                Enabled = false
            };
        }
        public static Watcher GetFake_master_Bitcoin_RSI()
        {
            return new Watcher
            {
                WatcherId = 2,
                UserId = "cesarbmx",
                CurrencyId = "btc",
                CreatorId = "cesarbmx",
                IndicatorId = "RSI",
                Value = 2,
                Quantity = 200,
                Buy = 15,
                Sell = 8,
                AverageBuy = 0,
                AverageSell = 0,
                Price = 5000,
                EntryPrice = 10,
                ExitPrice = 30,
                Status = WatcherStatus.SELL,
                Enabled = false
            };
        }
        public static List<Watcher> GetFake_List()
        {
            return new List<Watcher>
            {
                GetFake_master_Bitcoin_Price(),
                GetFake_master_Bitcoin_RSI()
            };
        }
    }
}
