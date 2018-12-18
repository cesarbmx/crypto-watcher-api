using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Indicator : Entity
    {
        public string IndicatorId => Id;
        public string UserId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Formula { get; private set; }

        public Indicator() { }
        public Indicator(string userId, string name, string description, string formula)
        {
            Id = UrlHelper.BuildUrl(userId, name);
            UserId = userId;
            Name = name;
            Description = description;
            Formula = formula;
            CreatedBy = userId;
        }

        public Indicator Update(string name, string description, string formula)
        {
            Name = name;
            Description = description;
            Formula = formula;

            return this;
        }
    }
}
