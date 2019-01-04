using System;


namespace CryptoWatcher.Domain.Models
{
    public class DataPoint : IEntity
    {
        public string Id => LineId.ToString();
        public Guid LineId { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
        public string TargetId { get; private set; }
        public string IndicatorId { get; private set; }
        public string UserId { get; private set; }
        public decimal? Value { get; private set; }
        public decimal? AverageBuy { get; private set; }
        public decimal? AverageSell { get; private set; }
        public DateTime Time { get; private set; }

        public DataPoint() { }
        public DataPoint(
            IndicatorType indicatorType,
            string targetId,
            string indicatorId,
            string userId,
            decimal? value,
            decimal? averageBuy,
            decimal? averageSell,
            DateTime time)
        {
            LineId = Guid.NewGuid();
            IndicatorType = indicatorType;
            TargetId = targetId;
            IndicatorId = indicatorId;
            UserId = userId;
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
            Time = time;
        }

        public DataPoint Set(decimal? value, decimal? averageBuy, decimal? averageSell)
        {
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;

            return this;
        }
    }
}
