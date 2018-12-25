using Topshelf;


namespace CryptoWatcher.Service.Configuration
{
    internal static class ServiceConfig
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<CryptoWatcherService>(service =>
                {
                    service.ConstructUsing(s => new CryptoWatcherService());
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
