using System.Collections.Generic;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Application.Responses
{
    public class Chart
    {
        public string ChartId { get; set; }
        public string CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string IndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public string UserId { get; set; }
        public List<ChartColumn> Columns { get; set; }
        public List<ChartRow> Rows { get; set; }
    }
}
