using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class WatcherSettingsFakeResponse
    {
        public static WatcherSettings GetFake_PriceChangeWatcher()
        {
            return new WatcherSettings(5000, 5500);
        }
        public static WatcherSettings GetFake_HypeWatcher()
        {
            return new WatcherSettings(5, 2);
        }
    }
}
