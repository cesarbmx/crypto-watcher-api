using System.ComponentModel.DataAnnotations;


namespace CryptoWatcher.Application.Requests
{
    public class AddUser
    {
        [Required] public string UserId { get; set; }
    }
}
