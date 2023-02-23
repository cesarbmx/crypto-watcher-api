﻿using CesarBmx.CryptoWatcher.Persistence.Contexts;
using CesarBmx.Shared.Api.Configuration;

namespace CesarBmx.CryptoWatcher.Api2.Configuration
{
    public static class DataSeedingConfig
    {
        public static IServiceCollection ConfigureDataSeeding(this IServiceCollection services)
        {
            services.ConfigureSharedDataSeeding<MainDbContext>();

            return services;
        }
    }
}
