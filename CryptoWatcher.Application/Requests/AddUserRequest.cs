


using System.ComponentModel.DataAnnotations;

namespace CryptoWatcher.Application.Requests
{
    public class AddUserRequest
    {
        [Required] public string UserId { get; set; }
    }
}
