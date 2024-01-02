using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.FakeRequests
{
    public static class FakeAddUser
    {
        public static AddUserRequest GetFake_master()
        {
            return new AddUserRequest
            {
                UserId = "cesarbmx",
                PhoneNumber = "+34 666868686"
            };
        }       
    }
}
