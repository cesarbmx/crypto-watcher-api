using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.UI.Models;
using LineChart = CryptoWatcher.UI.Models.LineChart;


namespace CryptoWatcher.UI.Builders
{
    public static class LineChartBuilder
    {
        public static LineChartViewModel BuildLineChartViewModel(List<LineChartResponse> lineChartsResponse)
        {
            var lineChartViewModel = new LineChartViewModel();
            foreach (var lineChartResponse in lineChartsResponse)
            {
                    var lineChart = new LineChart
                    {
                        LineChartId = lineChartResponse.LineChartId,
                        TargetName = lineChartResponse.TargetName,
                        IndicatorName = lineChartResponse.IndicatorName,
                        Rows = BuildRows(lineChartResponse.Rows)
                    };
                    lineChartViewModel.LineCharts.Add(lineChart);
            }

            // Return
            return lineChartViewModel;
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
