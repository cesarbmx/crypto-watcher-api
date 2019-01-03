using System.Collections.Generic;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Application.Responses
{
    public class ChartResponse
    {
        public string ChartId { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public string TargetId { get; set; }
        public string TargetName { get; set; }
        public string IndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public string UserId { get; set; }
        public List<ChartColumn> Columns { get; set; }
        public List<ChartRow> Rows { get; set; }
    }
}
