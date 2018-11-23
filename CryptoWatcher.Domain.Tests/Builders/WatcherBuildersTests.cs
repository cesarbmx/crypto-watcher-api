using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoWatcher.Domain.Builders;


namespace CryptoWatcher.Domain.Tests.Builders
{
    [TestClass]
    public class WatcherBuildersTests
    {
        [TestMethod]
        public void Hype_1()
        {
            // Arrange
            var values = new decimal[] {2, -10, -10, -10, -15};
            var value = values[0];

            // Act
            var result1 = WatcherBuilders.BuildHype(value, values);

            // Assert
            Assert.AreEqual(true, result1 >= 0);
        }
        [TestMethod]
        public void Hype_2()
        {
            // Arrange
            var values = new decimal[] { -5, 1, 1, 1, 6 };
            var value = values[0];

            // Act
            var result1 = WatcherBuilders.BuildHype(value, values);

            // Assert
            Assert.AreEqual(true, result1 == 0);
        }
    }
}
