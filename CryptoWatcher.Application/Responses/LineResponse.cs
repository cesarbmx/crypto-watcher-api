using System;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Application.Responses
{
    public class LineResponse
    {
        public IndicatorType IndicatorType { get; set; }
        public string TargetId { get; set; }
        public string IndicatorId { get; set; }
        public string UserId { get; set; }
        public decimal Value { get; set; }
        public decimal? AverageBuy { get; set; }
        public decimal? AverageSell { get; set; }
        public DateTime Time { get; set; }
    }
}
