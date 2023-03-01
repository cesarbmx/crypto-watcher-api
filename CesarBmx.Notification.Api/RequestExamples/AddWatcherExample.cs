using CesarBmx.Notification.Application.FakeRequests;
using CesarBmx.Notification.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.RequestExamples
{
    public class AddWatcherExample : IExamplesProvider<AddWatcher>
    {
        public AddWatcher GetExamples()
        {
            return FakeAddWatcher.GetFake_RSI();
        }
    }
}