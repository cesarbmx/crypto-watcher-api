using CryptoWatcher.Application.Responses;
using MediatR;
using Newtonsoft.Json;


namespace CryptoWatcher.Application.Requests
{
    public class UpdateIndicatorRequest : IRequest<IndicatorResponse>
    {
        [JsonIgnore]
        public string IndicatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Formula { get; set; }
    }
}
