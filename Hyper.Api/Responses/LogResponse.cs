using System;
using System.Collections.Generic;


namespace Hyper.Api.Responses
{
    public class LogResponse: List<string>
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
