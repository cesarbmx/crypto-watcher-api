using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class NotificationResponseExample : IExamplesProvider<Notification>
    {
        public Notification GetExamples()
        {
            return FakeNotification.GetFake_Master();
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