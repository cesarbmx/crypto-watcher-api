using System;
using CesarBmx.Shared.Domain.Entities;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Domain.Models
{
    public class Line : IEntity
    {
        public string Id => LineId.ToString();
        public Guid LineId { get; private set; }
        public string CurrencyId { get; private set; }
        public string IndicatorId { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
        public string UserId { get; private set; }
        public decimal? Value { get; private set; }
        public decimal? AverageBuy { get; private set; }
        public decimal? AverageSell { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Line() { }
        public Line(
            string currencyId,
            string indicatorId,
            IndicatorType indicatorType,
            string userId,
            decimal? value,
            decimal? averageBuy,
            decimal? averageSell,
            DateTime time)
        {
            LineId = Guid.NewGuid();
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            IndicatorType = indicatorType;
            UserId = userId;
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
            CreatedAt = time;
        }

        public Line Set(decimal? value, decimal? averageBuy, decimal? averageSell)
        {
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;

            return this;
        }
    }
}
