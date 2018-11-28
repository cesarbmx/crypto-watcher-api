using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.ConsoleApp.Configuration
{
    public static class Log4NetConfig
    {
        public static IServiceCollection ConfigureLog4Net(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(cfg =>
                cfg.AddLog4Net("log4net.config").AddConfiguration(configuration.GetSection("Logging")));

            return services;
        }
    }
}
