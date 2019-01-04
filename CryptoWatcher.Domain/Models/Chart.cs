using System.Collections.Generic;


namespace CryptoWatcher.Domain.Models
{
    public class LineChart
    {
        public string LineChartId { get; private set; }
        public string CurrencyId { get; private set; }
        public string CurrencyName { get; private set; }
        public string IndicatorId { get; private set; }
        public string IndicatorName { get; private set; }
        public List<LineChartColumn> Columns { get; private set; }
        public List<LineChartRow> Rows { get; private set; }

        public LineChart() { }
        public LineChart(
            string currencyId,
            string currencyName,
            string indicatorId,
            string indicatorName,
            List<LineChartColumn> columns, List<LineChartRow> rows)
        {
            LineChartId = currencyId + "-" + indicatorId;
            CurrencyId = currencyId;
            CurrencyName = currencyName;
            IndicatorId = indicatorId;
            IndicatorName = indicatorName;
            Columns = columns;
            Rows = rows;
        }
    }
}
