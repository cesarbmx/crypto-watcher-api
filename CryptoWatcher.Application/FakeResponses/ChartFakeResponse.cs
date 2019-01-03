using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class ChartFakeResponse
    {
        public static ChartResponse GetFake_1()
        {
            return new ChartResponse
            {
                ChartId = "bitcoin-hype",
                IndicatorType = IndicatorType.CurrencyIndicator,
                TargetId = "bitcoin",
                TargetName = "Bitcoin",
                IndicatorId = "hype",
                IndicatorName = "Hype",
                UserId = "master",
                Columns = ChartBuilder.BuildChartColumns(),
                Rows = new List<ChartRow> { new ChartRow(DateTime.Now, 6.4m, 8m, 4m), new ChartRow(DateTime.Now.AddMinutes(1), 6.6m, 8m, 4m) }
            };
        }
        public static ChartResponse GetFake_2()
        {
            return new ChartResponse
            {
                ChartId = "eos-hype",
                IndicatorType = IndicatorType.CurrencyIndicator,
                TargetId = "eos",
                TargetName = "EOS",
                IndicatorId = "hype",
                IndicatorName = "Hype",
                UserId = "master",
                Columns = ChartBuilder.BuildChartColumns(),
                Rows = new List<ChartRow> { new ChartRow(DateTime.Now, 8.4m, 8m, 4m), new ChartRow(DateTime.Now.AddMinutes(1), 8.6m, 8m, 4m) }
            };
        }
        public static List<ChartResponse> GetFake_List()
        {
            return new List<ChartResponse>
            {
                GetFake_1(),
                GetFake_2()
            };
        }
    }
}
