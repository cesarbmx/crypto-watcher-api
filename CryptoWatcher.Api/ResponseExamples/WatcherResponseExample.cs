using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
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