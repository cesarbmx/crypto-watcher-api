using System;

namespace CesarBmx.CryptoWatcher.Domain.Models
{
    public class UserLog
    {
        public Guid LogId { get; private set; }
        public string UserId { get; private set; }
        public string Action { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public UserLog() { }
        public UserLog (
            Guid logId, 
            string userId,
            string action,
            string text, 
            DateTime createdAt)
        {
            LogId = logId;
            UserId = userId;
            Action = action;
            Description = text;
            CreatedAt = createdAt;
        }
    }
}
