using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class NotificationResponseExample : IExamplesProvider<NotificationResponse>
    {
        public NotificationResponse GetExamples()
        {
            return NotificationFakeResponse.GetFake_master();
        }
    }
    public class NotificationListResponseExample : IExamplesProvider<List<NotificationResponse>>
    {
        public List<NotificationResponse> GetExamples()
        {
            return NotificationFakeResponse.GetFake_List();
        }
    }
}