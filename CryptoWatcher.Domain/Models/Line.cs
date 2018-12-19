using System;
using CryptoWatcher.Shared.Helpers;


namespace CryptoWatcher.Domain.Models
{
    public class Line : IEntity
    {
        public string Id => LineId;
        public string LineId { get; private set; }
        public string CurrencyId { get; private set; }
        public string IndicatorId { get; private set; }
        public decimal Value { get; private set; }
        public decimal AverageBuy { get; private set; }
        public decimal AverageSell { get; private set; }
        public string CreatedBy { get; private set; }
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
            var iso8601TimeWithoutMilliseconds = time.ToString("O").Substring(0, 19);
            LineId = UrlHelper.BuildSeoFriendlyUrl(currencyId, indicatorId) + iso8601TimeWithoutMilliseconds;
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
            CreatedBy = "system";
            Time = time;
        }
    }
}
