using CesarBmx.Notification.Application.FakeRequests;
using CesarBmx.Notification.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.RequestExamples
{
    public class AddUserExample : IExamplesProvider<AddUser>
    {
        public AddUser GetExamples()
        {
            return FakeAddUser.GetFake_master();
        }
    }
}