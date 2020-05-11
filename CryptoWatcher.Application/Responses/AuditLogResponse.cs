using System;


namespace CryptoWatcher.Application.Responses
{
    public class AuditLogResponse
    {
        public Guid AuditLogId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Json { get; set; }
    }
}
