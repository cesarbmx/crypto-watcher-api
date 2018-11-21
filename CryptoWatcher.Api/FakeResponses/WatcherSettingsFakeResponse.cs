using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class WatcherSettingsFakeResponse
    {
        public static WatcherSettings GetFake_PriceWatcher()
        {
            return new WatcherSettings("bitcoin", 5000, 5500);
        }
        public static WatcherSettings GetFake_HypeWatcher()
        {
            return new WatcherSettings("bitcoin", 5, 2);
        }
    }
}
