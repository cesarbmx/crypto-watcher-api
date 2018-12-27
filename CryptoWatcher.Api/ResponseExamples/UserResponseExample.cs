using CryptoWatcher.Application.Users.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class UserResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return UserFakeResponse.GetFake_Cesarbmx();
        }
    }
    public class UserListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return UserFakeResponse.GetFake_List();
        }
    }
}