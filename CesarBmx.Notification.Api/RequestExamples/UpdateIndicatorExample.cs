using CesarBmx.Notification.Application.FakeRequests;
using CesarBmx.Notification.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.RequestExamples
{
    public class UpdateIndicatorExample : IExamplesProvider<UpdateIndicator>
    {
        public UpdateIndicator GetExamples()
        {
            return FakeUpdateIndicator.GetFake_RSI();
        }
    }
}