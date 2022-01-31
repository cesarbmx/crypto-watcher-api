using System;


namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class Transaction<TRequest, TResponse,TStatus>
    {
        public Guid TransactionId { get; set; }
        public string Event { get; set; }
        public DateTime Timestamp { get; set; }
        public TStatus Status { get; set; }
        public TRequest Request { get; set; }
        public TResponse Response { get; set; }

        public Transaction(TRequest request, TResponse response, TStatus status)
        {
            Event = typeof(TRequest).Name;
            TransactionId = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            Status = status;
            Request = request;
            Response = response;
           
        }
    }
}
