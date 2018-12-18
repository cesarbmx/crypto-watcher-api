using System;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Shared.Helpers;


namespace CryptoWatcher.Domain.Models
{
    public class Line : IEntity
    {
        public string Id { get; private set; }
        public string CurrencyId { get; private set; }
        public string IndicatorId { get; private set; }
        public decimal Value { get; private set; }
        public decimal AverageBuy { get; private set; }
        public decimal AverageSell { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Line() { }
        public Line(
            string currencyId,
            string indicatorId,
            decimal value,
            decimal averageBuy,
            decimal averageSell,
            DateTime creationTime)
        {
            Id = UrlHelper.BuildUrl(currencyId, indicatorId) + creationTime.ToString("O");
            CurrencyId = currencyId;
            IndicatorId = indicatorId;
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
            CreatedBy = "system";
            CreationTime = creationTime;
        }
    }
}
