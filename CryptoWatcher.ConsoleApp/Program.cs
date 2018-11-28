using System;
using System.IO;
using CryptoWatcher.ConsoleApp.Configuration;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
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

            // Start app
            using (new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
                Console.ReadLine();
            }
        }
    }
}
