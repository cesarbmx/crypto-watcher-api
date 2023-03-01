using System.Collections.Generic;
using CesarBmx.Notification.Domain.Models;


namespace CesarBmx.Notification.Application.Responses
{
    public class Chart
    {
        public string ChartId { get; set; }
        public string CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string IndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public List<ChartColumn> Columns { get; set; }
        public List<ChartRow> Rows { get; set; }
    }
}
