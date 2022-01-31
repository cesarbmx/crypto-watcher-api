using System;
using Newtonsoft.Json;


namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class Response<TRequest, TResponse,TStatus>
    {
        public string Event => typeof(TRequest).Name;
        public Guid TransactionId { get; set; }
        public DateTime Timestamp { get; set; }
        public TStatus Status { get; set; }
        [JsonIgnore] public TRequest Request { get; set; }
        public TResponse Data { get; set; }
    }
}
