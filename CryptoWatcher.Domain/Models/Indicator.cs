using System;
using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Indicator : IEntity
    {
        public string Id { get; private set; }
        public string IndicatorId => Id;
        public string UserId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Formula { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Indicator() { }
        public Indicator(string userId, string name, string description, string formula)
        {
            Id = UrlHelper.BuildUrl(userId, name);
            UserId = userId;
            Name = name;
            Description = description;
            Formula = formula;
            CreatedBy = userId;
            CreationTime = DateTime.Now;
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
