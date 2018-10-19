

using System;

namespace Hyper.Domain.Models
{
    public class Log
    {
        public Guid Id { get; private set; }
        public LogLevel LogLevel { get; private set; }
        public Event Event { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Log() { }
        public Log(LogLevel logLevel, Event @event)
        {
            Id = Guid.NewGuid();
            LogLevel = logLevel;
            Event = @event;
            CreationTime = DateTime.Now;
        }
    }
}
