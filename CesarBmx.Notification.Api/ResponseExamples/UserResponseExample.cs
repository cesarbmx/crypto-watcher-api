using System.Collections.Generic;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Notification.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
{
    public class UserResponseExample : IExamplesProvider<User>
    {
        public User GetExamples()
        {
            return FakeUser.GetFake_master();
        }
    }
    public class UserListResponseExample : IExamplesProvider<List<User>>
    {
        public List<User> GetExamples()
        {
            return FakeUser.GetFake_List();
        }
    }
}