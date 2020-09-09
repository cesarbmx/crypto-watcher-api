using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.ModelBuilders
{
    public static class ChartBuilder
    {
        public static List<Chart> BuildCharts(List<Currency> currencies, List<Indicator> indicators, List<Line> lines)
        {
            // Line charts
            var charts = new List<Chart>();

            // For each currency
            foreach (var currency in currencies)
            {
                // For each indicator
                foreach (var indicator in indicators)
                {
                    // Grab lines for the given currency and indicator
                    var filteredLines = lines.Where(x =>
                        x.IndicatorId == indicator.IndicatorId &&
                        x.CurrencyId == currency.CurrencyId).ToList();

                    // Build the chart columns
                    var chartColumns = BuildChartColumns();
                    // Build the chart rows
                    var chartRows = BuildChartRows(filteredLines);
                    // Build the chart line
                    var chart = new Chart(currency.CurrencyId, currency.Name, indicator.IndicatorId, indicator.Name, chartColumns, chartRows);

                    // Add the line chart to the list
                    charts.Add(chart);
                }
            }

            // Return
            return charts;
        }
        public static List<ChartRow> BuildChartRows(List<Line> lines)
        {
            // Line chart rows
            var chartRows = new List<ChartRow>();

            // For each line
            foreach (var line in lines)
            {
                var chartRow = new ChartRow(line.Time, line.Value, line.AverageBuy, line.AverageSell);
                chartRows.Add(chartRow);
            }

            // Return
            return chartRows;
        }
        public static List<ChartColumn> BuildChartColumns()
        {           
            // Return
            return new List<ChartColumn>
            {
                new ChartColumn("DateTime", "Time"),
                new ChartColumn("string", "Value"),
                new ChartColumn("string", "Average buy"),
                new ChartColumn("string", "Average sell")
            };
        }
    }
}
