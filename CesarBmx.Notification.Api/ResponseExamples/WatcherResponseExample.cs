using System.Collections.Generic;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Notification.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
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