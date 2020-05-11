using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoWatcher.Domain.ModelBuilders;
using CryptoWatcher.Tests.FakeModels;


namespace CryptoWatcher.Tests.ModelBuilders
{
    [TestClass]
    public class IndicatorBuilderTests
    {
        [TestMethod]
        public void BuildHypes_1()
        {
            // Arrange
            var values = new decimal?[] {2, -10, -10, -10, -15};

            // Act
            IndicatorBuilder.BuildHypes(values);

            // Assert
            Assert.AreEqual(true, values[0] >= 0);
            Assert.AreEqual(true, values[1] == 0);
            Assert.AreEqual(true, values[2] == 0);
            Assert.AreEqual(true, values[3] == 0);
            Assert.AreEqual(true, values[4] == 0);
        }
        [TestMethod]
        public void BuildHypes_2()
        {
            // Arrange
            var values = new decimal?[] { 5, 1, 1, 1, -5 };

            // Act
            IndicatorBuilder.BuildHypes(values);

            // Assert
            Assert.AreEqual(true, values[0] >= 0);
            Assert.AreEqual(true, values[1] >= 0);
            Assert.AreEqual(true, values[2] >= 0);
            Assert.AreEqual(true, values[3] >= 0);
            Assert.AreEqual(true, values[4] == 0);
        }
        [TestMethod]
        public void BuildHypes_3()
        {
            // Arrange
            var values = new decimal?[] { 6, 1, 1, 1, 1 };

            // Act
            IndicatorBuilder.BuildHypes(values);

            // Assert
            Assert.AreEqual(true, values[0] >= 0);
            Assert.AreEqual(true, values[1] == 0);
            Assert.AreEqual(true, values[2] == 0);
            Assert.AreEqual(true, values[3] == 0);
            Assert.AreEqual(true, values[4] == 0);
        }
        [TestMethod]
        public void BuildHypes_4()
        {
            // Arrange
            var values = new decimal?[] { 1, -6, -6, -6, -6 };

            // Act
            IndicatorBuilder.BuildHypes(values);

            // Assert
            Assert.AreEqual(true, values[0] >= 0);
            Assert.AreEqual(true, values[1] == 0);
            Assert.AreEqual(true, values[2] == 0);
            Assert.AreEqual(true, values[3] == 0);
            Assert.AreEqual(true, values[4] == 0);
        }
        [TestMethod]
        public void BuildHypes_5()
        {
            // Arrange
            var values = new decimal?[] { 100, 0, 0, 0, 0 };

            // Act
            IndicatorBuilder.BuildHypes(values);

            // Assert
            Assert.AreEqual(true, values[0] >= 0);
            Assert.AreEqual(true, values[1] == 0);
            Assert.AreEqual(true, values[2] == 0);
            Assert.AreEqual(true, values[3] == 0);
            Assert.AreEqual(true, values[4] == 0);
        }
        [TestMethod]
        public void BuildHypes_6()
        {
            // Arrange
            var values = new decimal?[] { 50, 0, 0, 0, -50 };

            // Act
            IndicatorBuilder.BuildHypes(values);

            // Assert
            Assert.AreEqual(true, values[0] >= 0);
            Assert.AreEqual(true, values[1] == 0);
            Assert.AreEqual(true, values[2] == 0);
            Assert.AreEqual(true, values[3] == 0);
            Assert.AreEqual(true, values[4] == 0);
        }
        [TestMethod]
        public void BuildDependencyLevel_Hype()
        {
            // Arrange
            var indicatorId = "hype";
            var allIndicatorDependencies = FakeIndicatorDependencies.GetFakeIndicatorDependencies();

            // Act
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(indicatorId, allIndicatorDependencies);

            // Assert
            Assert.AreEqual(2, dependencyLevel);
        }
        [TestMethod]
        public void BuildDependencyLevel_PriceChange24Hrs()
        {
            // Arrange
            var indicatorId = "price-change-24hrs";
            var allIndicatorDependencies = FakeIndicatorDependencies.GetFakeIndicatorDependencies();

            // Act
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(indicatorId, allIndicatorDependencies);

            // Assert
            Assert.AreEqual(1, dependencyLevel);
        }
        [TestMethod]
        public void BuildDependencyLevel_Price()
        {
            // Arrange
            var indicatorId = "price";
            var allIndicatorDependencies = FakeIndicatorDependencies.GetFakeIndicatorDependencies();

            // Act
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(indicatorId, allIndicatorDependencies);

            // Assert
            Assert.AreEqual(0, dependencyLevel);
        }
    }
}
