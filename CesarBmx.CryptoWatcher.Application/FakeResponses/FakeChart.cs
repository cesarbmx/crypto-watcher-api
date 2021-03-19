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
                ChartId = "BTC-Master.PRICE",
                CurrencyId = "BTC",
                CurrencyName = "Bitcoin",
                IndicatorId = "Master.PRICE",
                IndicatorName = "Price",
                Columns = ChartBuilder.BuildChartColumns(),
                Rows = new List<ChartRow> { new ChartRow(DateTime.UtcNow.StripSeconds(), 6.4m, 8m, 4m), new ChartRow(DateTime.UtcNow.StripSeconds().AddMinutes(1), 6.6m, 8m, 4m) }
            };
        }
        public static Responses.Chart GetFake_EOS_Price()
        {
            return new Responses.Chart
            {
                ChartId = "EOS-Master.PRICE",
                CurrencyId = "EOS",
                CurrencyName = "EOS",
                IndicatorId = "Master.PRICE",
                IndicatorName = "Price",
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
