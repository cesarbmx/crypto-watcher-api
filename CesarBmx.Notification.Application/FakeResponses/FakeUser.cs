using System.Collections.Generic;
using CesarBmx.Notification.Application.Responses;

namespace CesarBmx.Notification.Application.FakeResponses
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
        public static User GetFake_cesar12()
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
                GetFake_cesar12()
            };
        }
    }
}
