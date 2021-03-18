using System.Collections.Generic;


namespace CesarBmx.CryptoWatcher.Domain.Models
{
    public class Chart
    {
        public string ChartId { get; private set; }
        public string CurrencyId { get; private set; }
        public string CurrencyName { get; private set; }
        public string IndicatorId { get; private set; }
        public string IndicatorName { get; private set; }
        public string UserId { get; private set; }
        public List<ChartColumn> Columns { get; private set; }
        public List<ChartRow> Rows { get; private set; }

        public Chart() { }
        public Chart(
            string currencyId,
            string currencyName,
            string userId,
            string indicatorId,
            string indicatorName,
            List<ChartColumn> columns, List<ChartRow> rows)
        {
            ChartId = currencyId + "-" + userId + "-" + indicatorId;
            CurrencyId = currencyId;
            CurrencyName = currencyName;
            IndicatorId = indicatorId;
            IndicatorName = indicatorName;
            UserId = userId;
            Columns = columns;
            Rows = rows;
        }
    }
}
