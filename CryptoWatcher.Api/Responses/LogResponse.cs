using System;


namespace CryptoWatcher.Api.Responses
{
    public class LogResponse
    {
        public string Id { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
        public string Json { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
