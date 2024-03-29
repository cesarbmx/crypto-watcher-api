﻿using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.RequestExamples
{
    public class AddWatcherExample : IExamplesProvider<AddWatcherRequest>
    {
        public AddWatcherRequest GetExamples()
        {
            return FakeAddWatcher.GetFake_RSI();
        }
    }
}