using CryptoWatcher.Application.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class NotificationResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return NotificationFakeResponse.GetFake_Cesarbmx();
        }
    }
    public class NotificationListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return NotificationFakeResponse.GetFake_List();
        }
    }
}