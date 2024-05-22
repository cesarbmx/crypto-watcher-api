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
                UserId = "master"
            };
        }
        public static User GetFake_User1()
        {
            return new User
            {
                UserId = "cesarbmx"
            };
        }
        public static List<User> GetFake_List()
        {
            return new List<User>
            {
                GetFake_master(),
                GetFake_User1()
            };
        }
    }
}
