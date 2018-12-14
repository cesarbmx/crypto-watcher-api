using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Web.Configuration
{
    public static class Log4NetConfig
    {
        public static ILoggerFactory ConfigureLog4Net(this ILoggerFactory logger, IHostingEnvironment env)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            logger.AddLog4Net();

            return logger;
        }
    }
}
