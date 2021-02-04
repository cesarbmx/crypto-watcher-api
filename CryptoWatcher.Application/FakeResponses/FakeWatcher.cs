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
                WatcherId = "master_bitcoin_price",
                UserId = "master",
                IndicatorId = "price",
                CurrencyId = "btc",
                Value = 5000,
                Buy = 15,
                Sell = 8,
                AverageBuy = 0,
                AverageSell = 0,
                Status = WatcherStatus.BUY,
                Enabled = false
            };
        }
        public static Watcher GetFake_master_Bitcoin_RSI()
        {
            return new Watcher
            {
                WatcherId = "master_bitcoin_rsi",
                UserId = "master",
                IndicatorId = "RSI",
                CurrencyId = "btc",
                Value = 2,
                Buy = 15,
                Sell = 8,
                AverageBuy = 0,
                AverageSell = 0,
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
