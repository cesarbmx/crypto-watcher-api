using System.ComponentModel.DataAnnotations;


namespace CryptoWatcher.Application.Requests
{
    public class AddNotificationRequest
    {
        [Required] public string Id { get; set; }
        [Required] public string Message { get; set; }
    }
}
