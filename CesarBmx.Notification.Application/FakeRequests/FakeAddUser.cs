using CesarBmx.Notification.Application.Requests;


namespace CesarBmx.Notification.Application.FakeRequests
{
    public static class FakeAddUser
    {
        public static AddUser GetFake_master()
        {
            return new AddUser
            {
                UserId = "cesarbmx",
                PhoneNumber = "+34 666868686"
            };
        }       
    }
}
