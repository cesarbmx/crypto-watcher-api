using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class UserResponseExample : IExamplesProvider<UserResponse>
    {
        public UserResponse GetExamples()
        {
            return FakeUser.GetFake_master();
        }
    }
    public class UserListResponseExample : IExamplesProvider<List<UserResponse>>
    {
        public List<UserResponse> GetExamples()
        {
            return FakeUser.GetFake_List();
        }
    }
}