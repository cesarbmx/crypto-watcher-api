using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeChart
    {
        public static Responses.Chart GetFake_Bitcoin_Price()
        {
            return new Responses.Chart
            {
                ChartId = "btc-price",
                CurrencyId = "btc",
                CurrencyName = "Bitcoin",
                IndicatorId = "price",
                IndicatorName = "Price",
                UserId = "master",
                Columns = ChartBuilder.BuildChartColumns(),
                Rows = new List<ChartRow> { new ChartRow(DateTime.UtcNow.StripSeconds(), 6.4m, 8m, 4m), new ChartRow(DateTime.UtcNow.StripSeconds().AddMinutes(1), 6.6m, 8m, 4m) }
            };
        }
        public static Responses.Chart GetFake_EOS_Price()
        {
            return new Responses.Chart
            {
                ChartId = "eos-price",
                CurrencyId = "eos",
                CurrencyName = "EOS",
                IndicatorId = "price",
                IndicatorName = "Price",
                UserId = "master",
                Columns = ChartBuilder.BuildChartColumns(),
                Rows = new List<ChartRow> { new ChartRow(DateTime.UtcNow.StripSeconds(), 8.4m, 8m, 4m), new ChartRow(DateTime.UtcNow.StripSeconds().AddMinutes(1), 8.6m, 8m, 4m) }
            };
        }
        public static List<Responses.Chart> GetFake_List()
        {
            return new List<Responses.Chart>
            {
                GetFake_Bitcoin_Price(),
                GetFake_EOS_Price()
            };
        }
    }
}
