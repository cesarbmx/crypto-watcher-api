﻿using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.FakeRequests
{
    public static class FakeAddUser
    {
        public static AddUser GetFake_master()
        {
            return new AddUser
            {
                UserId = "CesarBmx",
                PhoneNumber = "+34 666868686"
            };
        }       
    }
}
