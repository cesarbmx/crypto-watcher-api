using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;

namespace CryptoWatcher.Application.Services
{
    public class SeedService
    {
        private readonly MainDbContext _mainDbContext;

        public SeedService(
            MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task Seed()
        {
            // Create master
            var master = new User("master");

            // Add master
            _mainDbContext.Users.Add(master);

            // Create indicators with their dependencies
            var price = new Indicator("price", IndicatorType.CurrencyIndicator, "master", "Price", "", "", new List<IndicatorDependency>(), 0);
            var priceChange24Hrs = new Indicator("price-change-24hrs", IndicatorType.CurrencyIndicator, "master", "Price change 24Hrs", "desc", "f()",
                new List<IndicatorDependency>()
                {
                    new IndicatorDependency("price", price)
                }, 0);
            var hype = new Indicator("hype", IndicatorType.CurrencyIndicator, "master", "Hype", "desc", "f()",
                new List<IndicatorDependency>()
                {
                    new IndicatorDependency("hype", priceChange24Hrs)
                }, 0);


            // Add indicators
            _mainDbContext.Indicators.AddRange(new List<Indicator>{ price , priceChange24Hrs, hype });

            await _mainDbContext.SaveChangesAsync();
        }
    }
}
