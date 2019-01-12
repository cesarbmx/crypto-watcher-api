using System.Collections.Generic;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class UserFakeResponse
    {
        public static UserResponse GetFake_master()
        {
            return new UserResponse
            {
                UserId = "master"
            };
        }
        public static UserResponse GetFake_cesar12()
        {
            return new UserResponse
            {
                UserId = "cesar12"
            };
        }
        public static List<UserResponse> GetFake_List()
        {
            return new List<UserResponse>
            {
                GetFake_master(),
                GetFake_cesar12()
            };
        }
    }
}
