using Microsoft.VisualStudio.TestTools.UnitTesting;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Tests.Domain.FakeModels;


namespace CesarBmx.CryptoWatcher.Tests.Domain.Builders
{
    [TestClass]
    public class BuildDependencyLevel
    {
        [TestMethod]
        public void Test_Hype()
        {
            // Arrange
            var userId = "Master";
            var indicatorId = "HYPE";
            var allIndicatorDependencies = FakeIndicatorDependencies.GetFakeIndicatorDependencies();

            // Act
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(userId, indicatorId, allIndicatorDependencies);

            // Assert
            Assert.AreEqual(2, dependencyLevel);
        }

        [TestMethod]
        public void Test_PriceChange24Hrs()
        {
            // Arrange
            var userId = "Master";
            var indicatorId = "PRICE_CHANGE_24hrs";
            var allIndicatorDependencies = FakeIndicatorDependencies.GetFakeIndicatorDependencies();

            // Act
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(userId, indicatorId, allIndicatorDependencies);

            // Assert
            Assert.AreEqual(1, dependencyLevel);
        }

        [TestMethod]
        public void Test_Price()
        {
            // Arrange
            var userId = "Master";
            var indicatorId = "PRICE";
            var allIndicatorDependencies = FakeIndicatorDependencies.GetFakeIndicatorDependencies();

            // Act
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(userId, indicatorId, allIndicatorDependencies);

            // Assert
            Assert.AreEqual(0, dependencyLevel);
        }
    }
}
