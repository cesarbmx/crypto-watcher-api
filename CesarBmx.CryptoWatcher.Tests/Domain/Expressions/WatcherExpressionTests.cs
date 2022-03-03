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
        public void Test_WatcherNotSet()
        {
            // Arrange
            var watchersBuyingAndSelling = FakeWatchers.GetWatchersNotSet();

            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherNotSet()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherSet()
        {
            // Arrange
            var watchersBuyingAndSelling = FakeWatchers.GetWatchersNotSet();

            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherNotSet()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
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
        public void Test_WatcherLiquidated()
        {
            // Arrange
            var watchersBuyingAndSelling = FakeWatchers.GetWatchersLiquidated();

            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherSold()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
    }
}
