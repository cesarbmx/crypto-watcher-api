using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Domain.Models;


namespace CesarBmx.CryptoWatcher.Api.ViewModelBuilders
{
    public static class ChartBuilder
    {
        public static List<ViewModels.Chart> BuildCharts(List<Application.Responses.Chart> chartsResponse)
        {
            var charts = new List<ViewModels.Chart>();
            foreach (var chartResponse in chartsResponse)
            {
                var chart = new ViewModels.Chart
                {
                    ChartId = chartResponse.ChartId,
                    CurrencyName = chartResponse.CurrencyName,
                    IndicatorName = chartResponse.IndicatorName,
                    Rows = BuildRows(chartResponse.Rows)
                };
                charts.Add(chart);
            }

            // Return
            return charts;
        }

        public static string BuildRows(List<ChartRow> chartRows)
        {
            // Rows
            var rows = string.Empty;
            foreach (var chartRow in chartRows)
            {
                var time = chartRow.Time;
                var value = chartRow.Value.HasValue ? chartRow.Value.ToString() : "null";
                var averageBuy = chartRow.AverageBuy.HasValue ? chartRow.AverageBuy.ToString() : "null";
                var averageSell = chartRow.AverageSell.HasValue ? chartRow.AverageSell.ToString() : "null";
  
                var dateTime = $"new Date({time.Year},{time.Month:D2},{time.Day:D2},{time.Hour:D2},{time.Minute:D2})";
                rows += ", " + $"[{dateTime}, {value}, {averageBuy}, {averageSell}]";
            }

            if (!string.IsNullOrEmpty(rows)) rows = rows.Substring(2);

            // Return
            return rows;
        }
    }
}
