﻿using CesarBmx.Shared.Api.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api2.Configuration
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.UseSharedAuthentication(configuration);

            return services;
        }
    }
}