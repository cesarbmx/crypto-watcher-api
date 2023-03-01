using CesarBmx.Notification.Application.FakeRequests;
using CesarBmx.Notification.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.RequestExamples
{
    public class UpdateWatcherExample : IExamplesProvider<SetWatcher>
    {
        public SetWatcher GetExamples()
        {
            return FakeSetWatcher.GetFake_1();
        }
    }
}