using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.Shared.Serialization.Helpers;
using System;


namespace CesarBmx.CryptoWatcher.Domain.Models
{
    public class Event 
    {
        public int EventId { get; private set; }
        public EventType EventType { get; private set; }
        public string Json { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Event() { }
        public Event(EventType eventType, object json, DateTime createdAt)
        {
            EventId = 0;
            EventType = eventType;
            Json = JsonConvertHelper.SerializeObject(json);
            CreatedAt = createdAt;
        }
    }
}
