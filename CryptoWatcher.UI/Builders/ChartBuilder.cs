using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.UI.Models;


namespace CryptoWatcher.UI.Builders
{
    public static class ChartBuilder
    {
        public static ChartViewModel BuildChartViewModel(List<CurrencyResponse> currencies, List<IndicatorResponse> indicators,  List<LineResponse> lines)
        {
            var chartViewModel = new ChartViewModel();
            var index = 0;
            foreach (var currency in currencies)
            {
                foreach (var indicator in indicators)
                {
                    var chart = new Chart
                    {
                        Index = index,
                        CurrencyId = currency.CurrencyId,
                        CurrencyName = currency.Name,
                        IndicatorId = indicator.IndicatorId,
                        IndicatorName = indicator.Name,
                        Rows = BuildRows(lines.Where(x=>x.CurrencyId == currency.CurrencyId && x.IndicatorId == indicator.IndicatorId).ToList())
                    };
                    chartViewModel.Charts.Add(chart);
                    index++;
                }
            }

            // Return
            return chartViewModel;
        }

        public static string BuildRows(List<LineResponse> lines)
        {
            // Rows
            var rows = string.Empty;
            foreach (var line in lines)
            {
                var dateTime =
                    $"new Date({line.Time.Year},{line.Time.Month},{line.Time.Day},{line.Time.Hour},{line.Time.Minute})";
                rows += ", " + $"[{dateTime}, {line.Value}, {line.AverageBuy}, {line.AverageSell}]";
            }

            if (!string.IsNullOrEmpty(rows)) rows = rows.Substring(2);

            // Return
            return rows;
        }
    }
}
