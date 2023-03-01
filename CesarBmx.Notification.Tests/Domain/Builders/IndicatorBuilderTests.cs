using Microsoft.VisualStudio.TestTools.UnitTesting;
using CesarBmx.Notification.Domain.Builders;
using CesarBmx.Notification.Tests.Domain.FakeModels;


namespace CesarBmx.Notification.Tests.Domain.Builders
{
    [TestClass]
    public class IndicatorBuilderTests
    {

        #region BuildDependencyLevel

        [TestMethod]
        public void Test_BuildDependencyLevel_Hype()
        {
            // Arrange
            var indicatorId = "master.HYPE";
            var allIndicatorDependencies = FakeIndicatorDependency.GetFakeIndicatorDependencies();

            // Act
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(indicatorId, allIndicatorDependencies);

            // Assert
            Assert.AreEqual(2, dependencyLevel);
        }

        [TestMethod]
        public void Test_BuildDependencyLevel_PriceChange24Hrs()
        {
            // Arrange
            var indicatorId = "master.PRICE_CHANGE_24H";
            var allIndicatorDependencies = FakeIndicatorDependency.GetFakeIndicatorDependencies();

            // Act
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(indicatorId, allIndicatorDependencies);

            // Assert
            Assert.AreEqual(1, dependencyLevel);
        }

        [TestMethod]
        public void Test_BuildDependencyLevel_Price()
        {
            // Arrange
            var indicatorId = "master.PRICE";
            var allIndicatorDependencies = FakeIndicatorDependency.GetFakeIndicatorDependencies();

            // Act
            var dependencyLevel = IndicatorBuilder.BuildDependencyLevel(indicatorId, allIndicatorDependencies);

            // Assert
            Assert.AreEqual(0, dependencyLevel);
        }

        #endregion

        #region BuildHypes

        [TestMethod]
        public void Test_BuildHypes_1()
        {
            // Arrange
            var values = new decimal[] { 2, -10, -10, -10, -15 };

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
        public void Test_BuildHypes_2()
        {
            // Arrange
            var values = new decimal[] { 5, 1, 1, 1, -5 };

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
        public void Test_BuildHypes_3()
        {
            // Arrange
            var values = new decimal[] { 6, 1, 1, 1, 1 };

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
        public void Test_BuildHypes_4()
        {
            // Arrange
            var values = new decimal[] { 1, -6, -6, -6, -6 };

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
        public void Test_BuildHypes_5()
        {
            // Arrange
            var values = new decimal[] { 100, 0, 0, 0, 0 };

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
        public void Test_BuildHypes_6()
        {
            // Arrange
            var values = new decimal[] { 50, 0, 0, 0, -50 };

            // Act
            IndicatorBuilder.BuildHypes(values);

            // Assert
            Assert.AreEqual(true, values[0] >= 0);
            Assert.AreEqual(true, values[1] == 0);
            Assert.AreEqual(true, values[2] == 0);
            Assert.AreEqual(true, values[3] == 0);
            Assert.AreEqual(true, values[4] == 0);
        }

        #endregion
    }
}
