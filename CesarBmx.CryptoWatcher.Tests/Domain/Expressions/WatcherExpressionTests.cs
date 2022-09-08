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
            var watchersBuyingAndSelling = FakeWatcher.GetWatchersNotSet();

            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherNotSet()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherSet()
        {
            // Arrange
            var watchersBuyingAndSelling = FakeWatcher.GetWatchersNotSet();

            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherNotSet()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherBuyingOrSelling()
        {
            // Arrange
            var watchersBuyingAndSelling = FakeWatcher.GetWatchersBuyingAndSelling();
            
            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherBuyingOrSelling().Compile()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherHolding()
        {
            // Arrange
            var watchersBuyingAndSelling = FakeWatcher.GetWatchersHolding();

            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherHolding()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherSold()
        {
            // Arrange
            var watchersBuyingAndSelling = FakeWatcher.GetWatchersSold();

            // Act
            var filter = watchersBuyingAndSelling.Where(WatcherExpression.WatcherSold()).ToList();

            // Assert
            Assert.AreEqual(watchersBuyingAndSelling.Count, filter.Count);
        }
    }
}
