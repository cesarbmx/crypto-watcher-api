using System.ComponentModel.DataAnnotations;
using CryptoWatcher.Application.Indicators.Responses;
using MediatR;

namespace CryptoWatcher.Application.Indicators.Requests
{
    public class AddIndicatorRequest : IRequest<IndicatorResponse>
    {
        [Required] public string IndicatorId { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Formula { get; set; }
    }
}
