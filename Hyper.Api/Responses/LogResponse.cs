using System;


namespace Hyper.Api.Responses
{
    public class LogResponse
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public string Action { get; set; }
        public string Json { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
