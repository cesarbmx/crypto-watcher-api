using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CesarBmx.CryptoWatcher.Tests.Domain.FakeModels;
using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.CryptoWatcher.Domain.Builders;

namespace CesarBmx.CryptoWatcher.Tests.Domain.Builders
{
    [TestClass]
    public class WatcherBuilderTests
    {
        [TestMethod]
        public void Test_WatcherNotSet()
        {
            // Act
            var status = WatcherBuilder.BuildWatcherStatus(
                currentStatus: WatcherStatus.NOT_SET,
                buy: null,
                sell: null,
                value: 3000,
                hasBuyingOrder: false,
                hasSellingOrder: false,
                isBuyingOrderConfirmed: false,
                isSellingOrderConfirmed: false);

            // Assert
            Assert.AreEqual(WatcherStatus.NOT_SET, status);
        }
        [TestMethod]
        public void Test_WatcherSet_WithSell()
        {
            // Act
            var status = WatcherBuilder.BuildWatcherStatus(
                currentStatus: WatcherStatus.NOT_SET,
                buy: 2000,
                sell: 3000,
                value: 3000,
                hasBuyingOrder: false,
                hasSellingOrder: false,
                isBuyingOrderConfirmed: false,
                isSellingOrderConfirmed: false);

            // Assert
            Assert.AreEqual(WatcherStatus.SET, status);
        }
        [TestMethod]
        public void Test_WatcherSet_WithoutSell()
        {
            // Act
            var status = WatcherBuilder.BuildWatcherStatus(
                currentStatus: WatcherStatus.NOT_SET,
                buy: 2000,
                sell: null,
                value:3000,
                hasBuyingOrder: false,
                hasSellingOrder: false,
                isBuyingOrderConfirmed: false,
                isSellingOrderConfirmed: false);

            // Assert
            Assert.AreEqual(WatcherStatus.SET, status);
        }
        [TestMethod]
        public void Test_WatcherBuying()
        {
            // Act
            var status = WatcherBuilder.BuildWatcherStatus(
                currentStatus: WatcherStatus.SET,
                buy: 2000,
                sell: null,
                value: 2000,
                hasBuyingOrder: false,
                hasSellingOrder: false,
                isBuyingOrderConfirmed: false,
                isSellingOrderConfirmed: false);

            // Assert
            Assert.AreEqual(WatcherStatus.BUYING, status);
        }
        [TestMethod]
        public void Test_WatcherBought()
        {
            // Act
            var status = WatcherBuilder.BuildWatcherStatus(
                currentStatus: WatcherStatus.BUYING,
                buy: 2000,
                sell: null,
                value: 2000,
                hasBuyingOrder: true,
                hasSellingOrder: false,
                isBuyingOrderConfirmed: false,
                isSellingOrderConfirmed: false);

            // Assert
            Assert.AreEqual(WatcherStatus.BOUGHT, status);
        }
        [TestMethod]
        public void Test_WatcherHolding()
        {
            // Act
            var status = WatcherBuilder.BuildWatcherStatus(
                currentStatus: WatcherStatus.BOUGHT,
                buy: 2000,
                sell: null,
                value: 3000,
                hasBuyingOrder: true,
                hasSellingOrder: false,
                isBuyingOrderConfirmed: true,
                isSellingOrderConfirmed: false);

            // Assert
            Assert.AreEqual(WatcherStatus.HOLDING, status);
        }
        [TestMethod]
        public void Test_WatcherSelling()
        {
            // Act
            var status = WatcherBuilder.BuildWatcherStatus(
                currentStatus: WatcherStatus.BOUGHT,
                buy: 2000,
                sell: 3000,
                value: 3000,
                hasBuyingOrder: true,
                hasSellingOrder: false,
                isBuyingOrderConfirmed: true,
                isSellingOrderConfirmed: false);

            // Assert
            Assert.AreEqual(WatcherStatus.SELLING, status);
        }
        [TestMethod]
        public void Test_WatcherSold()
        {
            // Act
            var status = WatcherBuilder.BuildWatcherStatus(
                currentStatus: WatcherStatus.SELLING,
                buy: 2000,
                sell: 3000,
                value: 4000,
                hasBuyingOrder: true,
                hasSellingOrder: true,
                isBuyingOrderConfirmed: true,
                isSellingOrderConfirmed: true);

            // Assert
            Assert.AreEqual(WatcherStatus.SOLD, status);
        }
    }
}
