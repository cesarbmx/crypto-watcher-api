using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CesarBmx.CryptoWatcher.Api2.Configuration
{
    public static class SerilogConfig
    {
        public static ILoggerFactory ConfigureSerilog(this IApplicationBuilder app, ILoggerFactory logger, IConfiguration configuration)
        {
            app.ConfigureSharedSerilog(logger, Assembly.GetExecutingAssembly(), configuration);
            
            return logger;
        }
    }
}
