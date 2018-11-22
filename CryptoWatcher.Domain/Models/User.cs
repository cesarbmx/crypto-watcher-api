

namespace CryptoWatcher.Domain.Models
{
    public class User : Entity
    {
        public string UserId { get; private set; }
       
        public User() { }
        public User(string userId)
        {
            UserId = userId;
        }
    }
}
