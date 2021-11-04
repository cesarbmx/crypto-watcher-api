using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeUser
    {
        public static User GetFake_master()
        {
            return new User
            {
                UserId = "Master"
            };
        }
        public static User GetFake_cesar12()
        {
            return new User
            {
                UserId = "CesarBmx"
            };
        }
        public static List<User> GetFake_List()
        {
            return new List<User>
            {
                GetFake_master(),
                GetFake_cesar12()
            };
        }
    }
}
