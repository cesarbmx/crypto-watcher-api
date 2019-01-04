using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class LineChartFakeResponse
    {
        public static LineChartResponse GetFake_1()
        {
            return new LineChartResponse
            {
                LineChartId = "bitcoin-hype",
                IndicatorType = IndicatorType.CurrencyIndicator,
                TargetId = "bitcoin",
                TargetName = "Bitcoin",
                IndicatorId = "hype",
                IndicatorName = "Hype",
                UserId = "master",
                Columns = LineChartBuilder.BuildLineChartColumns(),
                Rows = new List<LineChartRow> { new LineChartRow(DateTime.Now, 6.4m, 8m, 4m), new LineChartRow(DateTime.Now.AddMinutes(1), 6.6m, 8m, 4m) }
            };
        }
        public static LineChartResponse GetFake_2()
        {
            return new LineChartResponse
            {
                LineChartId = "eos-hype",
                IndicatorType = IndicatorType.CurrencyIndicator,
                TargetId = "eos",
                TargetName = "EOS",
                IndicatorId = "hype",
                IndicatorName = "Hype",
                UserId = "master",
                Columns = LineChartBuilder.BuildLineChartColumns(),
                Rows = new List<LineChartRow> { new LineChartRow(DateTime.Now, 8.4m, 8m, 4m), new LineChartRow(DateTime.Now.AddMinutes(1), 8.6m, 8m, 4m) }
            };
        }
        public static List<LineChartResponse> GetFake_List()
        {
            return new List<LineChartResponse>
            {
                GetFake_1(),
                GetFake_2()
            };
        }
    }
}
