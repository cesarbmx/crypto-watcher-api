using CesarBmx.Notification.Application.Requests;


namespace CesarBmx.Notification.Application.FakeRequests
{
    public static class FakeEnableWatcher
    {
        public static EnableWatcher GetFake_1()
        {
            return new EnableWatcher
            {
                WatcherId = 1,
                Enabled = true
            };
        }       
    }
}
