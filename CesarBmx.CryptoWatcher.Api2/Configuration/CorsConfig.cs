﻿using CesarBmx.Shared.Application.Settings;
using CesarBmx.Shared.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api2.Configuration
{
    public static class CorsConfig
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSharedCors(configuration);

            return services;
        }
        public static IApplicationBuilder ConfigureCors(this IApplicationBuilder app)
        {
            app.ConfigureSharedCors();

            return app;
        }
    }
}
