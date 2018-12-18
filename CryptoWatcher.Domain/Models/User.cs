


namespace CryptoWatcher.Domain.Models
{
    public class User : Entity
    {
        public string UserId => Id;

        public User() { }
        public User(string userId)
        {
            Id = userId;
            CreatedBy = userId;
        }
    }
}
