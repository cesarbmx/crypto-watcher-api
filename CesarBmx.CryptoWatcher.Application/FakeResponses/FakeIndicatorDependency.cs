using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeIndicatorDependency
    {
        public static IndicatorDependency GetFake_Price()
        {
            return new IndicatorDependency()
            {
                IndicatorId = "Master.RSI",
                UserId = "Master",
                Abbreviation = "PRICE",
            };
        }
        public static IndicatorDependency GetFake_RSI()
        {
            return new IndicatorDependency
            {
                IndicatorId = "Master.RSI",
                UserId = "Master",
                Abbreviation = "RSI"
            };
        }
        public static List<IndicatorDependency> GetFake_List()
        {
            return new List<IndicatorDependency>
            {
                GetFake_Price(),
                GetFake_RSI()
            };
        }
    }
}
