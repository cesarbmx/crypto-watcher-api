using System;


namespace CryptoWatcher.Domain.Models
{
    public class Line : IEntity
    {
        public string Id => LineId.ToString();
        public Guid LineId { get; private set; }
        public string CurrencyId { get; private set; }
        public string IndicatorId { get; private set; }
        public decimal Value { get; private set; }
        public decimal AverageBuy { get; private set; }
        public decimal AverageSell { get; private set; }
        public DateTime Time { get; private set; }

        public Line() { }
        public Line(
            string currencyId,
            string indicatorId,
            decimal value,
            decimal averageBuy,
            decimal averageSell,
            DateTime time)
        {
            LineId = Guid.NewGuid();
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
            Time = time;
        }
    }
}
