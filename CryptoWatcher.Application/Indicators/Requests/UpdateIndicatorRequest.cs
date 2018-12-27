using System.ComponentModel.DataAnnotations;
using CryptoWatcher.Application.Indicators.Responses;
using MediatR;
using Newtonsoft.Json;


namespace CryptoWatcher.Application.Indicators.Requests
{
    public class UpdateIndicatorRequest : IRequest<IndicatorResponse>
    {
        [JsonIgnore] public string IndicatorId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Formula { get; set; }
    }
}
