

using System.ComponentModel.DataAnnotations;

namespace CryptoWatcher.Application.Requests
{
    public class AddIndicatorRequest
    {
        [Required] public string IndicatorId { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Formula { get; set; }
    }
}
