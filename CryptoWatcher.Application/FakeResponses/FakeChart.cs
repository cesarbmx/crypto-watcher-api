using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Application.FakeResponses
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
                Rows = new List<ChartRow> { new ChartRow(DateTime.UtcNow.StripSeconds().StripSeconds(), 6.4m, 8m, 4m), new ChartRow(DateTime.UtcNow.StripSeconds().StripSeconds().AddMinutes(1), 6.6m, 8m, 4m) }
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
                Rows = new List<ChartRow> { new ChartRow(DateTime.UtcNow.StripSeconds().StripSeconds(), 8.4m, 8m, 4m), new ChartRow(DateTime.UtcNow.StripSeconds().StripSeconds().AddMinutes(1), 8.6m, 8m, 4m) }
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
