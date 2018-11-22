using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoWatcher.Domain.Builders;


namespace CryptoWatcher.Domain.Tests.Builders
{
    [TestClass]
    public class WatcherBuildersTests
    {
        [TestMethod]
        public void Hype_cannot_be_less_than_zero()
        {
            // Arrange
            var values = new decimal[] {-15, -10, -10, -10, 2};
            var value = values[0];

            // Act
            var result1 = WatcherBuilders.BuildHype(value, values);

            // Assert
            Assert.AreEqual(true, result1 >= 0);
        }
    }
}
