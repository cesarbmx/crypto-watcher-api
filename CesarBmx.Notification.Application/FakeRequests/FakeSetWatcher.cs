using CesarBmx.Notification.Application.Requests;


namespace CesarBmx.Notification.Application.FakeRequests
{
    public static class FakeSetWatcher
    {
        public static SetWatcher GetFake_1()
        {
            return new SetWatcher
            {
                WatcherId = 1,             
                Buy = 30000,
                Sell = 50000,
                Quantity = 100
            };
        }       
    }
}
