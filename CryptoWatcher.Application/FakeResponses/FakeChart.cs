using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.ModelBuilders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class FakeChart
    {
        public static Responses.LineChart GetFake_Bitcoin_Price()
        {
            return new Responses.LineChart
            {
                LineChartId = "bitcoin-price",
                IndicatorType = IndicatorType.CurrencyIndicator,
                CurrencyId = "bitcoin",
                CurrencyName = "Bitcoin",
                IndicatorId = "price",
                IndicatorName = "Price",
                UserId = "master",
                Columns = LineChartBuilder.BuildLineChartColumns(),
                Rows = new List<LineChartRow> { new LineChartRow(DateTime.Now, 6.4m, 8m, 4m), new LineChartRow(DateTime.Now.AddMinutes(1), 6.6m, 8m, 4m) }
            };
        }
        public static Responses.LineChart GetFake_EOS_Price()
        {
            return new Responses.LineChart
            {
                LineChartId = "eos-price",
                IndicatorType = IndicatorType.CurrencyIndicator,
                CurrencyId = "eos",
                CurrencyName = "EOS",
                IndicatorId = "price",
                IndicatorName = "Price",
                UserId = "master",
                Columns = LineChartBuilder.BuildLineChartColumns(),
                Rows = new List<LineChartRow> { new LineChartRow(DateTime.Now, 8.4m, 8m, 4m), new LineChartRow(DateTime.Now.AddMinutes(1), 8.6m, 8m, 4m) }
            };
        }
        public static List<Responses.LineChart> GetFake_List()
        {
            return new List<Responses.LineChart>
            {
                GetFake_Bitcoin_Price(),
                GetFake_EOS_Price()
            };
        }
    }
}
