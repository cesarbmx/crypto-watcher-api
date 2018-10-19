using System;


namespace Hyper.Api.Responses
{
    public class LogResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
