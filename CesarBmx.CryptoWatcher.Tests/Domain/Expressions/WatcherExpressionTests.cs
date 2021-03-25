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
    }
}
