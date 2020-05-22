using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class NotificationResponseExample : IExamplesProvider<Notification>
    {
        public Notification GetExamples()
        {
            return FakeNotification.GetFake_master();
        }
    }
    public class NotificationListResponseExample : IExamplesProvider<List<Notification>>
    {
        public List<Notification> GetExamples()
        {
            return FakeNotification.GetFake_List();
        }
    }
}