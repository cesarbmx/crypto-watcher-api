using System;


namespace CryptoWatcher.Api.Responses
{
    public class LogResponse
    {
        public string LogId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationTime { get; set; }
        public string Json { get; set; }
    }
}
