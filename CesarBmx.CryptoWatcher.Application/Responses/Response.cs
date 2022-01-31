using System;
using Newtonsoft.Json;


namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class Response<TRequest, TResponse,TStatus>
    {
        public string Event { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime Timestamp { get; set; }
        [JsonIgnore] public TRequest Request { get; set; }
        public TStatus Status { get; set; }
        public TResponse Data { get; set; }

        public Response(TRequest request, TResponse response, TStatus status)
        {
            Event = typeof(TRequest).Name;
            TransactionId = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            Request = request;
            Status = status;
            Data = response;
           
        }
    }
}
