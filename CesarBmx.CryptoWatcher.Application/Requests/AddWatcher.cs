using System.ComponentModel.DataAnnotations;


namespace CesarBmx.CryptoWatcher.Application.Requests
{
    public class AddWatcher 
    {
        [Required] public string UserId { get; set; }
        [Required] public string CurrencyId { get; set; }
        [Required] public string IndicatorId { get; set; }
        [Required] public bool Enabled { get; set; }
    }
}
