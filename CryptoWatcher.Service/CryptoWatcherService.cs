


using System;
using System.IO;
using CryptoWatcher.Service.Configuration;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Service
{
    public class CryptoWatcherService
    {
        public void Start()
        {
            // write code here that runs when the Windows Service starts up.  

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

            // Start app
            using (new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
                Console.ReadLine();
            }
        }
        public void Stop()
        {
            // write code here that runs when the Windows Service stops.  
        }
    }
}
