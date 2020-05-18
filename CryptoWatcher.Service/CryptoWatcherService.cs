using Hangfire;

namespace CryptoWatcher.Service
{
    public class CryptoWatcherService
    {
        private BackgroundJobServer _backgroundJobServer;

        public void Start()
        {
            _backgroundJobServer = new BackgroundJobServer();
        }
        public void Stop()
        {
            _backgroundJobServer.Dispose();
        }
    }
}
