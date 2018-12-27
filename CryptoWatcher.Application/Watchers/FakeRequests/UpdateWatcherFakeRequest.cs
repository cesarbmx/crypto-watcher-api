using System;
using CryptoWatcher.Application.Watchers.Requests;


namespace CryptoWatcher.Application.Watchers.FakeRequests
{
    public static class UpdateWatcherFakeRequest
    {
        public static UpdateWatcherRequest GetFake_1()
        {
            return new UpdateWatcherRequest
            {
                WatcherId = Guid.NewGuid(),             
                Buy = 15,
                Sell = 8,
                Enabled = true
            };
        }       
    }
}
