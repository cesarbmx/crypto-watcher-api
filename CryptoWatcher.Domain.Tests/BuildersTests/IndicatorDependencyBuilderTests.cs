using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Tests.BuildersTests
{
    [TestClass]
    public class IndicatorDependencyBuilderTests
    {
        [TestMethod]
        public void BuildDependencyLevel_1()
        {
            // Arrange
            var dependencyLevels = new List<IndicatorDependency>
            {
                new IndicatorDependency("2", "1"),
                new IndicatorDependency("3", "1"),
                new IndicatorDependency("3", "2"),
                new IndicatorDependency("4", "3"),
                new IndicatorDependency("5", "3")
            };

            // Act
            IndicatorDependencyBuilder.BuildDependencyLevel(dependencyLevels);

            // Assert
            Assert.AreEqual(true, dependencyLevels[0].Level == 1);
            Assert.AreEqual(true, dependencyLevels[1].Level == 1);
            Assert.AreEqual(true, dependencyLevels[2].Level == 2);
            Assert.AreEqual(true, dependencyLevels[3].Level == 3);
            Assert.AreEqual(true, dependencyLevels[3].Level == 3);
        }

    }
}
