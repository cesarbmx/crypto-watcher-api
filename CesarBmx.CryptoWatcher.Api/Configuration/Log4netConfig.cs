using System.Reflection;
using CesarBmx.Shared.Api.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class Log4NetConfig
    {
        public static ILoggerFactory ConfigureLog4Net(this ILoggerFactory logger, IHostEnvironment environment, IConfiguration configuration)
        {
             logger.ConfigureSharedLog4Net(Assembly.GetExecutingAssembly(), environment, configuration);
            
            return logger;
        }
    }
}
