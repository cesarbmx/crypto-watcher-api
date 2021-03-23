using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.FakeRequests
{
    public static class FakeAddWatcher
    {
        public static AddWatcher GetFake_RSI()
        {
            return new AddWatcher
            {
                UserId = "CesarBmx",
                CurrencyId = "BTC", 
                IndicatorId = "Master.PRICE",
                Enabled = true
            };
        }       
    }
}
