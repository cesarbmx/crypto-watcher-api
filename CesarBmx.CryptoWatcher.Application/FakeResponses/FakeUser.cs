using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeUser
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
                UserId = "cesarbmx"
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
