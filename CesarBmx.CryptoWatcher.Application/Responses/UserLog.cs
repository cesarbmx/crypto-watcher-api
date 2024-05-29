using CesarBmx.CryptoWatcher.Domain.Types;
using System;


namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class UserLogResponse
    {
        public Guid LogId { get; set; }
        public string UserId { get; set; }
        public ActionType ActionType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
