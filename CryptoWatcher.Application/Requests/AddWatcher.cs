using System.ComponentModel.DataAnnotations;


namespace CryptoWatcher.Application.Requests
{
    public class AddWatcher 
    {
        [Required] public string UserId { get; set; }
        [Required] public string IndicatorId { get; set; }
        [Required] public string CurrencyId { get; set; }
        public decimal? Buy { get; set; }
        public decimal? Sell { get; set; }
        [Required] public bool Enabled { get; set; }
    }
}
