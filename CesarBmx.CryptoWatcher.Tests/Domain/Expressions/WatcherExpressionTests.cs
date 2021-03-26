using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Tests.Domain.FakeModels;


namespace CesarBmx.CryptoWatcher.Tests.Domain.Expressions
{
    [TestClass]
    public class WatcherExpressionTests
    {
        [TestMethod]
        public void Test_WatcherBuyingOrSelling()
        {
            // Arrange
            var watchersBuyingAndSelling = FakeWatchers.GetWatchersBuyingAndSelling();
            
            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherBuyingOrSelling().Compile()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherHolding()
        {
            // Arrange
            var watchersBuyingAndSelling = FakeWatchers.GetWatchersHolding();

            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherHolding()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherWatcherLiquidated()
        {
            // Arrange
            var watchersBuyingAndSelling = FakeWatchers.GetWatchersLiquidated();

            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherLiquidated()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
    }
}
