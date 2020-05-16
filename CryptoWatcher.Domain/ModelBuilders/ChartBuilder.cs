using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.ModelBuilders
{
    public static class LineChartBuilder
    {
        public static List<LineChart> BuildLineCharts(List<Currency> currencies, List<Indicator> indicators, List<Line> lines)
        {
            // Line charts
            var lineCharts = new List<LineChart>();

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
                    var lineChartColumns = BuildLineChartColumns();
                    // Build the chart rows
                    var lineChartRows = BuildLineChartRows(filteredLines);
                    // Build the chart line
                    var lineChart = new LineChart(currency.CurrencyId, currency.Name, indicator.IndicatorId, indicator.Name, lineChartColumns, lineChartRows);

                    // Add the line chart to the list
                    lineCharts.Add(lineChart);
                }
            }

            // Return
            return lineCharts;
        }
        public static List<LineChartRow> BuildLineChartRows(List<Line> lines)
        {
            // Line chart rows
            var lineChartRows = new List<LineChartRow>();

            // For each line
            foreach (var line in lines)
            {
                var lineChartRow = new LineChartRow(line.Time, line.Value, line.AverageBuy, line.AverageSell);
                lineChartRows.Add(lineChartRow);
            }

            // Return
            return lineChartRows;
        }
        public static List<LineChartColumn> BuildLineChartColumns()
        {           
            // Return
            return new List<LineChartColumn>
            {
                new LineChartColumn("DateTime", "Time"),
                new LineChartColumn("string", "Value"),
                new LineChartColumn("string", "Average buy"),
                new LineChartColumn("string", "Average sell")
            };
        }
    }
}
