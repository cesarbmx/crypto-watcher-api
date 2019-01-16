using System;


namespace CryptoWatcher.Domain.Models
{
    public class DataPoint : IEntity
    {
        public string Id => LineId.ToString();
        public Guid LineId { get; private set; }
        public string TargetId { get; private set; }
        public string IndicatorId { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
        public string UserId { get; private set; }
        public decimal? Value { get; private set; }
        public decimal? AverageBuy { get; private set; }
        public decimal? AverageSell { get; private set; }
        public bool IsCurrent { get; private set; }
        public DateTime Time { get; private set; }

        public DataPoint() { }
        public DataPoint(
            string targetId,
            string indicatorId,
            IndicatorType indicatorType,
            string userId,
            decimal? value,
            decimal? averageBuy,
            decimal? averageSell,
            bool isCurrent,
            DateTime time)
        {
            LineId = Guid.NewGuid();
            TargetId = targetId;
            IndicatorId = indicatorId;
            IndicatorType = indicatorType;
            UserId = userId;
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
            IsCurrent = isCurrent;
            Time = time;
        }

        public DataPoint Set(decimal? value, decimal? averageBuy, decimal? averageSell)
        {
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;

            return this;
        }
        public DataPoint SetAsNoLongerCurrent()
        {
            IsCurrent = false;

            return this;
        }
    }
}
