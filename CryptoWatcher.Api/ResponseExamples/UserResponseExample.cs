using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class UserResponseExample : IExamplesProvider<UserResponse>
    {
        public UserResponse GetExamples()
        {
            return UserFakeResponse.GetFake_master();
        }
    }
    public class UserListResponseExample : IExamplesProvider<List<UserResponse>>
    {
        public List<UserResponse> GetExamples()
        {
            return UserFakeResponse.GetFake_List();
        }
    }
}