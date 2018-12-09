

namespace CryptoWatcher.Domain.Models
{
    public class User : Entity
    {
        public string UserId => Id;

        public User(string id)
        {
            Id = id;
        }
    }
}
