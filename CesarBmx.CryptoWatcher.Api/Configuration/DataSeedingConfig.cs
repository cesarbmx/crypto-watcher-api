using CesarBmx.CryptoWatcher.Persistence.Contexts;
using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class DataSeedingConfig
    {
        public static IApplicationBuilder ConfigureDataSeeding(this IApplicationBuilder app)
        {
            return app.ConfigureSharedDataSeeding<MainDbContext>();
        }
    }
}
