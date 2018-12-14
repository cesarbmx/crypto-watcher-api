

using System;

namespace CryptoWatcher.Web.Models
{
    public class ChartValue
    {
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
    }
}