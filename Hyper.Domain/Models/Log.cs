using System;

namespace Hyper.Domain.Models
{
    public class Log
    {
        public Guid Id { get; private set; }
        public string Message { get; private set; }
        public DateTime CreationTime { get; private set; }
        
        public Log() { }
        public Log(string message)
        {
            Id = Guid.NewGuid();
            Message = message;
            CreationTime = DateTime.Now;
        }
    }
}
