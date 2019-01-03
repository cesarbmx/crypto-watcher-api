using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class ChartBuilder
    {
        public static List<Chart> BuildCharts(List<Currency> currencies, List<Indicator> indicators, List<Line> lines)
        {
            var charts = new List<Chart>();
            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    var filteredLines = lines.Where(x =>  
                        x.IndicatorType == indicator.IndicatorType &&
                        x.IndicatorId == indicator.IndicatorId &&
                        x.TargetId == currency.CurrencyId).ToList();

                    var chartColumns = BuildChartColumns();
                    var chartRows = BuildChartRows(filteredLines);
                    var chart = new Chart(currency.CurrencyId, currency.Name, indicator.IndicatorId, indicator.Name, chartColumns, chartRows);
                    charts.Add(chart);
                }
            }

            // Return
            return charts;
        }
        public static List<ChartRow> BuildChartRows(List<Line> lines)
        {
            // Chart rows
            var chartRows = new List<ChartRow>();
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
                new ChartColumn("string", "Hype"),
                new ChartColumn("string", "Average buy"),
                new ChartColumn("string", "Average sell")
            };
        }
    }
}
