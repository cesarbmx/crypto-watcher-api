using Hangfire;

namespace CesarBmx.Notification.Service
{
    public class NotificationService
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
