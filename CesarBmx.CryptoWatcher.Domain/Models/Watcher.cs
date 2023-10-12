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
        public decimal? Buy { get; private set; }
        public decimal? Sell { get; private set; }
        public decimal? AverageBuy { get; private set; }
        public decimal? AverageSell { get; private set; }
        public decimal? Value { get; private set; }
        public decimal? Price { get; private set; }
        public decimal? Quantity { get; private set; }
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
            bool enabled,
            DateTime createdAt)
        {
            WatcherId = 0;
            UserId = userId;
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            Enabled = enabled;
            CreatedAt = createdAt;
        }

        public Watcher Update(Watcher watcher)
        {
            UserId = watcher.UserId;
            CurrencyId = watcher.CurrencyId;
            IndicatorId = watcher.IndicatorId;
            Status = watcher.Status;
            Buy = watcher.Buy;
            Sell = watcher.Sell;
            AverageBuy = watcher.AverageBuy;
            AverageSell = watcher.AverageSell;
            Value = watcher.Value;
            Price = watcher.Price;
            Quantity = watcher.Quantity;
            BuyingOrder = watcher.BuyingOrder;
            SellingOrder = watcher.SellingOrder;
            Enabled = watcher.Enabled;
            CreatedAt = watcher.CreatedAt;

            return this;
        }
        public Watcher Set(decimal? buy, decimal? sell, decimal? quantity)
        {
            // Buy can be set before buying
            if (Status == WatcherStatus.NOT_SET)
            {
                Buy = buy;
                Sell = sell;
                Quantity = quantity;
            }

            // Sell can be set before selling
            if (Status == WatcherStatus.BUYING)
            {
                Sell = sell;
            }

            // Set status
            SetStatus();

            // Return
            return this;
        }
        public Watcher Enable(bool enabled)
        {
            Enabled = enabled;

            return this;
        }
        public Watcher Sync(Watcher watcher)
        {         
            return Sync(watcher.AverageBuy, watcher.AverageSell, watcher.Value, watcher.Price);
        }
        public Watcher Sync(decimal? averageBuyValue, decimal? averageSellValue, decimal? value, decimal? price)
        {
            AverageBuy = averageBuyValue;
            AverageSell = averageSellValue;
            Price = price;
            Value = value;

            SetStatus();

            return this;
        }
        public Watcher SetStatus()
        {
            // If the buy has not been set
            if (Buy == null)
            {
                Status = WatcherStatus.NOT_SET;
                return this;
            }

            // If the buy has been set, it has not been executed yet and does not meet the buy, then it is just set
            if (Buy != null && BuyingOrder == null && (Value == null || Buy < Value))
            {
                Status = WatcherStatus.SET;
                return this;
            }

            // If the buy has been set, it has not been executed yet, but meets the buy, then it is buying
            if (Buy != null && BuyingOrder == null && Buy >= Value)
            {
                Status = WatcherStatus.BUYING;
                BuyingOrder = new Order();
                return this;
            }

            // if buy has already happened, but sell has not, and meets the sell
            if (BuyingOrder != null && SellingOrder == null && Sell != null && Sell <= Value)
            {
                Status = WatcherStatus.SELLING;
                SellingOrder = new Order();
                return this;
            }

            // if buy has already happened and sell is not set, then it is just holding
            if (BuyingOrder != null && SellingOrder == null && Sell == null)
            {
                Status = WatcherStatus.HOLDING;
                return this;
            }            

            // If sell has already happened 
            if (SellingOrder != null)
            {
                Status = WatcherStatus.SOLD;
                return this;
            }

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
        public Watcher ConfirmBuyOrder(decimal price, DateTime executedAt)
        {
            BuyingOrder.ConfirmOrder(price, executedAt);

            SetStatus();

            return this;
        }
        public Watcher ConfirmSellOrder(decimal price, DateTime executedAt)
        {
            SellingOrder.ConfirmOrder(price, executedAt);

            SetStatus();

            return this;
        }
    }
}
