using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class LineChartBuilder
    {
        public static List<LineChart> BuildLineCharts(List<Currency> currencies, List<Indicator> indicators, List<Line> lines)
        {
            var lineCharts = new List<LineChart>();
            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    var filteredLines = lines.Where(x =>  
                        x.IndicatorType == indicator.IndicatorType &&
                        x.IndicatorId == indicator.IndicatorId &&
                        x.CurrencyId == currency.CurrencyId).ToList();

                    var lineChartColumns = BuildLineChartColumns();
                    var lineChartRows = BuildLineChartRows(filteredLines);
                    var lineChart = new LineChart(currency.CurrencyId, currency.Name, indicator.IndicatorId, indicator.Name, lineChartColumns, lineChartRows);
                    lineCharts.Add(lineChart);
                }
            }

            // Return
            return lineCharts;
        }
        public static List<LineChartRow> BuildLineChartRows(List<Line> lines)
        {
            // LineChart rows
            var lineChartRows = new List<LineChartRow>();
            foreach (var line in lines)
            {
                var lineChartRow = new LineChartRow(line.CreatedAt, line.Value, line.AverageBuy, line.AverageSell);
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
