using System.IO;
using CryptoWatcher.Service.Configuration;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Service
{
    public class CryptoWatcherService
    {
        private BackgroundJobServer _backgroundJobServer;

        public void Start()
        {
            // Configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            // Configure services
            var serviceProvider = new ServiceCollection()
                .ConfigureAutomapper()
                .ConfigureDependencies(configuration)
                .ConfigureLog4Net(configuration)
                .BuildServiceProvider();

            // Configure hangfire
            serviceProvider.ConfigureHangfire(configuration);

            // Data seeding
            serviceProvider.ConfigureDataSeeding();

            _backgroundJobServer = new BackgroundJobServer();
        }
        public void Stop()
        {
            _backgroundJobServer.Dispose();
        }
    }
}
