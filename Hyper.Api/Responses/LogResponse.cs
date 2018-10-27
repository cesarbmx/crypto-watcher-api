using System;


namespace Hyper.Api.Responses
{
    public class LogResponse
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ActionName { get; set; }
        public string ModelJson { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
