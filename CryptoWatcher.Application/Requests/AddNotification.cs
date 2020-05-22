using System.ComponentModel.DataAnnotations;


namespace CryptoWatcher.Application.Requests
{
    public class AddNotification
    {
        [Required] public string Id { get; set; }
        [Required] public string Message { get; set; }
    }
}
