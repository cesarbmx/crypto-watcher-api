using System.IO;
using CesarBmx.Shared.Api.Configuration;
using CesarBmx.Shared.Api.Helpers;
using CesarBmx.CryptoWatcher.Service.Configuration;
using Hangfire;
using Hangfire.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Topshelf;


namespace CesarBmx.CryptoWatcher.Service
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
                .AddLogging()
                .ConfigureAutomapper()
                .ConfigureDependencies(configuration)
                .ConfigureLog4Net(configuration)
                .AddSingleton<IConfiguration>(configuration)
                .BuildServiceProvider();
            
            // Hangfire Logger
            var scopeFactory = (IServiceScopeFactory)serviceProvider.GetService(typeof(IServiceScopeFactory));
            GlobalConfiguration.Configuration.UseLogProvider(new HangfireLoggerProvider());
            GlobalConfiguration.Configuration.UseActivator(new AspNetCoreJobActivator(scopeFactory));

            // Configure Hangfire
            serviceProvider.ConfigureHangfire(configuration);

            // TopSelf
           HostFactory.Run(configure =>
            {
                configure.Service<CryptoWatcherService>(service =>
                {
                    service.ConstructUsing(_ => new CryptoWatcherService());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                //Setup Account that window service use to run.  
                configure.RunAsLocalSystem();
                configure.SetServiceName("CryptoWatcherService");
                configure.SetDisplayName("CryptoWatcher Service");
                configure.SetDescription("It runs the hangfire recurring jobs");
            });

          

        }
    }
}
