using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class SerilogConfig
    {
        public static ILoggerFactory ConfigureSerilog(this ILoggerFactory logger, IConfiguration configuration)
        {
            logger.ConfigureSharedSerilog(configuration, Assembly.GetExecutingAssembly());

            return logger;
        }
    }
}
