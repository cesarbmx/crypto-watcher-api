using System;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Domain.Models
{
    public class Line
    {
        public DateTime Time { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public string IndicatorId { get; private set; }
        public decimal Value { get; private set; }
        public decimal? AverageBuy { get; private set; }
        public decimal? AverageSell { get; private set; }
        public Period Period { get; private set; }

        public Line() { }
        public Line(
            DateTime time,
            string userId,
            string currencyId,
            string indicatorId,
            decimal value,
            decimal? averageBuy,
            decimal? averageSell)
        {
            Time = time;
            UserId = userId;
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
            Period = LineBuilder.BuildPeriod(time);
        }
    }
}
