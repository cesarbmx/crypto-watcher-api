


using System;

namespace CryptoWatcher.Domain.Models
{
    public class LineChartRow
    {
        public DateTime Time { get; private set; }
        public decimal? Value { get; private set; }
        public decimal? AverageBuy { get; private set; }
        public decimal? AverageSell { get; private set; }


        public LineChartRow() { }
        public LineChartRow(DateTime time, decimal? value, decimal? averageBuy, decimal? averageSell)
        {
            Time = time;
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
        }
    }
}
