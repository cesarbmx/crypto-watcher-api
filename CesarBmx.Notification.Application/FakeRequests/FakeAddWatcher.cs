using CesarBmx.Notification.Application.Requests;


namespace CesarBmx.Notification.Application.FakeRequests
{
    public static class FakeAddWatcher
    {
        public static AddWatcher GetFake_RSI()
        {
            return new AddWatcher
            {
                UserId = "cesarbmx",
                CurrencyId = "BTC", 
                IndicatorId = "master.PRICE",
                Enabled = true
            };
        }       
    }
}
