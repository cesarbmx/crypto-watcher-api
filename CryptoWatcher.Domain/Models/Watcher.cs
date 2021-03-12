using System;
using CesarBmx.Shared.Domain.Models;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Domain.Models
{
    public class Watcher : IEntity<Watcher>

    {
        public string Id => UserId + "_" + CurrencyId + "_" + CreatorId + "_" + IndicatorId;

        public int WatcherId { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public string IndicatorId { get; private set; }
        public string CreatorId { get; private set; }
        public decimal? Value { get; private set; }
        public decimal? Buy { get; private set; }
        public decimal? Sell { get; private set; }
        public decimal? Quantity { get; private set; }
        public decimal? AverageBuy { get; private set; }
        public decimal? AverageSell { get; private set; }
        public decimal? Price { get; private set; }
        public decimal? EntryPrice { get; private set; }
        public decimal? ExitPrice { get; private set; }
        public decimal? Profit => WatcherBuilder.BuildProfit(EntryPrice, ExitPrice, Quantity);
        public bool Enabled { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public WatcherStatus Status => WatcherBuilder.BuildStatus(Value, Buy, Sell);

        public Watcher()
        {
        }

        public Watcher(
            string userId,
            string currencyId,
            string indicatorId,
            string creatorId,
            decimal? value,
            decimal? buy,
            decimal? sell,
            decimal? quantity,
            decimal? averageBuy,
            decimal? averageSell,
            decimal? price,
            bool enabled,
            DateTime createdAt)
        {
            WatcherId = 0;
            UserId = userId;
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            CreatorId = creatorId;
            Value = value;
            Buy = buy;
            Sell = sell;
            Quantity = quantity;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
            Price = price;
            Enabled = enabled;
            CreatedAt = createdAt;
        }

        public Watcher Update(Watcher watcher)
        {
            Value = watcher.Value;

            return this;
        }
        public Watcher Update(decimal buy, decimal sell, bool enabled)
        {
            Buy = buy;
            Sell = sell;
            Enabled = enabled;

            return this;
        }
        public Watcher Sync(decimal? value, decimal? averageBuyValue, decimal? averageSellValue, decimal? price)
        {
            Value = value;
            AverageBuy = averageBuyValue;
            AverageSell = averageSellValue;
            Price = price;

            return this;
        }
        public Watcher SetAsBought()
        {
            EntryPrice = Buy;

            return this;
        }
        public Watcher SetAsSold()
        {
            ExitPrice = Sell;

            return this;
        }
    }
}
