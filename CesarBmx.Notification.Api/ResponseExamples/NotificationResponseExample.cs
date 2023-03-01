using System.Collections.Generic;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Notification.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
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