using CesarBmx.Notification.Application.FakeRequests;
using CesarBmx.Notification.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.RequestExamples
{
    public class AddIndicatorExample : IExamplesProvider<AddIndicator>
    {
        public AddIndicator GetExamples()
        {
            return FakeAddIndicator.GetFake_RSI();
        }
    }
}