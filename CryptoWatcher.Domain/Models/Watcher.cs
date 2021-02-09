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
        public string CreatorId { get; private set; }
        public string IndicatorId { get; private set; }
        public decimal? Value { get; private set; }
        public decimal? Buy { get; private set; }
        public decimal? Sell { get; private set; }
        public decimal? Amount { get; private set; }
        public decimal? AverageBuy { get; private set; }
        public decimal? AverageSell { get; private set; }
        public decimal? Price { get; private set; }
        public bool Enabled { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public WatcherStatus Status => WatcherBuilder.BuildStatus(Value, Buy, Sell);

        public Watcher()
        {
        }

        public Watcher(
            string userId,
            string currencyId,
            string creatorId,
            string indicatorId,
            decimal? value,
            decimal? buy,
            decimal? sell,
            decimal? amount,
            decimal? averageBuy,
            decimal? averageSell,
            decimal? price,
            bool enabled,
            DateTime createdAt)
        {
            WatcherId = 0;
            UserId = userId;
            CreatorId = creatorId;
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            Value = value;
            Buy = buy;
            Sell = sell;
            Amount = amount;
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
        public Watcher Sync(decimal? value, decimal? averageBuyValue, decimal? averageSellValue)
        {
            Value = value;
            AverageBuy = averageBuyValue;
            AverageSell = averageSellValue;

            return this;
        }
        public Watcher ResetBuySell()
        {
            Buy = null;
            Sell = null;

            return this;
        }
    }
}
