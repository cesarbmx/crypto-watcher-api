using System;
using CesarBmx.Shared.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.Shared.Common.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using CesarBmx.CryptoWatcher.Domain.Expressions;

namespace CesarBmx.CryptoWatcher.Domain.Models
{
    public class Watcher : IEntity<Watcher>

    {
        public string Id => UserId + "_" + CurrencyId + "_" + IndicatorId;

        public int WatcherId { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public string IndicatorId { get; private set; }
        public WatcherStatus Status { get; private set; }
        public decimal? Value { get; private set; }
        public decimal? Buy { get; private set; }
        public decimal? Sell { get; private set; }
        public decimal? Quantity { get; private set; }
        public decimal? AverageBuy { get; private set; }
        public decimal? AverageSell { get; private set; }
        public decimal? Price { get; private set; }
        public Order BuyingOrder { get; private set; }
        public Order SellingOrder { get; private set; }
        public decimal? Profit => WatcherBuilder.BuildProfit(BuyingOrder?.Price, SellingOrder?.Price, Quantity);
        public bool Enabled { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Watcher()
        {
        }

        public Watcher(
            string userId,
            string currencyId,
            string indicatorId,
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
            Value = value;
            Buy = buy;
            Sell = sell;
            Quantity = quantity;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
            Price = price;
            Enabled = enabled;
            CreatedAt = createdAt;

            SetStatus();
        }

        public Watcher Update(Watcher watcher)
        {
            Value = watcher.Value;
            AverageBuy = watcher.AverageBuy;
            AverageSell = watcher.AverageSell;
            Price = watcher.Price;

            SetStatus();

            return this;
        }
        public Watcher Set(decimal buy, decimal? sell, decimal quantity)
        {
            // It can be fully changed only when it is not se
            if (Status == WatcherStatus.NOT_SET)
            {
                Buy = buy;
                Sell = sell;
                Quantity = quantity;
            }

            // We can still change the selling price if it has not been bought yet
            if (Status == WatcherStatus.BUYING)
            {
                Sell = sell;
            }

            SetStatus();
            return this;
        }
        public Watcher SetStatus()
        {
            // If buying price has not been set
            if (Buy == null) {
                Status = WatcherStatus.NOT_SET;
                return this;
            }

            // If buying order has not been set, then it is still buying
            if (BuyingOrder == null)
            {
                Status = WatcherStatus.BUYING;
                return this;
            }

            // if selling order has not been set, then it is still either holding or selling
            if (SellingOrder == null && Sell == null)
            {
                Status = WatcherStatus.HOLDING;
                return this;
            }
            if (SellingOrder == null && Sell != null) 
            {
                Status = WatcherStatus.SELLING;
                return this;
            }

            // If selling order has been set, then it is sold
            if (SellingOrder != null)
            {
                Status = WatcherStatus.SOLD;
                return this;
            }

            return this;
        }
        public Watcher Enable(bool enabled)
        {
            Enabled = enabled;

            return this;
        }
        public Watcher Sync(decimal? value, decimal? averageBuyValue, decimal? averageSellValue, decimal? price)
        {
            Value = value;
            AverageBuy = averageBuyValue;
            AverageSell = averageSellValue;
            Price = price;

            SetStatus();

            return this;
        }
        public Watcher SetAsBuying()
        {
            BuyingOrder = new Order();

            SetStatus();

            return this;
        }
        public Watcher SetAsSelling()
        {
            SellingOrder = new Order();

            SetStatus();

            return this;
        }
        public Watcher ConfirmOrder(decimal price, DateTime executedAt)
        {
            if (SellingOrder != null)
            {
                SellingOrder.ConfirmOrder(price, executedAt);
                SetStatus();
                return this;
            }
            if (BuyingOrder != null)
            {
                BuyingOrder.ConfirmOrder(price, executedAt);
                SetStatus();
                return this;
            }
            return this;
        }
        //public Watcher ConfirmBuy(decimal price, DateTime executedAt)
        //{
        //    BuyingOrder.ConfirmOrder(price, executedAt);

        //    SetStatus();

        //    return this;
        //}
        //public Watcher ConfirmSell(decimal price, DateTime executedAt)
        //{
        //    SellingOrder.ConfirmOrder(price, executedAt);

        //    SetStatus();

        //    return this;
        //}
    }
}
