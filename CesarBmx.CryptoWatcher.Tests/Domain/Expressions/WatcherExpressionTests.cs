using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CesarBmx.CryptoWatcher.Tests.Domain.FakeModels;
using CesarBmx.CryptoWatcher.Domain.Types;

namespace CesarBmx.CryptoWatcher.Tests.Domain.Expressions
{
    [TestClass]
    public class WatcherExpressionTests
    {
        [TestMethod]
        public void Test_WatcherNotSet()
        {
            // Arrange
            var watchers = FakeWatcher.GetWatchersNotSet();

            // Act
            var filter = watchers.Where(x=>x.Status == WatcherStatus.NOT_SET).ToList();

            // Assert
            Assert.AreEqual(watchers.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherSet()
        {
            // Arrange
            var watchers = FakeWatcher.GetWatchersSet();

            // Act
            var filter = watchers.Where(x => x.Status == WatcherStatus.SET).ToList();

            // Assert
            Assert.AreEqual(watchers.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherBuying()
        {
            // Arrange
            var watchers = FakeWatcher.GetWatchersBuying();

            // Act
            var filter = watchers.Where(x => x.Status == WatcherStatus.BUYING).ToList();

            // Assert
            Assert.AreEqual(watchers.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherHolding()
        {
            // Arrange
            var watchers = FakeWatcher.GetWatchersHolding();

            // Act
            var filter = watchers.Where(x => x.Status == WatcherStatus.HOLDING).ToList();

            // Assert
            Assert.AreEqual(watchers.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherSelling()
        {
            // Arrange
            var watchers = FakeWatcher.GetWatchersSelling();

            // Act
            var filter = watchers.Where(x => x.Status == WatcherStatus.SELLING).ToList();

            // Assert
            Assert.AreEqual(watchers.Count, filter.Count);
        }
        [TestMethod]
        public void Test_WatcherSold()
        {
            // Arrange
            var watchers = FakeWatcher.GetWatchersSold();

            // Act
            var filter = watchers.Where(x => x.Status == WatcherStatus.SOLD).ToList();

            // Assert
            Assert.AreEqual(watchers.Count, filter.Count);
        }
    }
}
