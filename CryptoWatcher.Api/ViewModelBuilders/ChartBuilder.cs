using System.Collections.Generic;
using CryptoWatcher.Api.ViewModels;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Api.ViewModelBuilders
{
    public static class ChartBuilder
    {
        public static List<Chart> BuildCharts(List<Application.Responses.LineChart> lineChartsResponse)
        {
            var charts = new List<Chart>();
            foreach (var lineChartResponse in lineChartsResponse)
            {
                var lineChart = new Chart
                {
                    LineChartId = lineChartResponse.LineChartId,
                    CurrencyName = lineChartResponse.CurrencyName,
                    IndicatorName = lineChartResponse.IndicatorName,
                    Rows = BuildRows(lineChartResponse.Rows)
                };
                charts.Add(lineChart);
            }

            // Return
            return charts;
        }

        public static string BuildRows(List<LineChartRow> lineChartRows)
        {
            // Rows
            var rows = string.Empty;
            foreach (var lineChartRow in lineChartRows)
            {
                var time = lineChartRow.Time;
                var value = lineChartRow.Value.HasValue ? lineChartRow.Value.ToString() : "null";
                var averagebuy = lineChartRow.AverageBuy.HasValue ? lineChartRow.AverageBuy.ToString() : "null";
                var averageSell = lineChartRow.AverageSell.HasValue ? lineChartRow.AverageSell.ToString() : "null";
  
                var dateTime = $"new Date({time.Year},{time.Month:D2},{time.Day:D2},{time.Hour:D2},{time.Minute:D2})";
                rows += ", " + $"[{dateTime}, {value}, {averagebuy}, {averageSell}]";
            }

            if (!string.IsNullOrEmpty(rows)) rows = rows.Substring(2);

            // Return
            return rows;
        }
    }
}
