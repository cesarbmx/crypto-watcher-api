

using System.ComponentModel.DataAnnotations;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Requests
{
    public class AddIndicatorRequest
    {
        public IndicatorType IndicatorType { get; set; }
        public string IndicatorId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Formula { get; set; }
    }
}
