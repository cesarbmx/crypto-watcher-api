using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Resources;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class WatcherResponseExample : IExamplesProvider<Watcher>
    {
        public Watcher GetExamples()
        {
            return FakeWatcher.GetFake_master_Bitcoin_RSI();
        }
    }
    public class WatcherListResponseExample : IExamplesProvider<List<Watcher>>
    {
        public List<Watcher> GetExamples()
        {
            return FakeWatcher.GetFake_List();
        }
    }
}