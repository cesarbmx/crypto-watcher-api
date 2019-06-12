using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Tests.ModelTests
{
    [TestClass]
    public class IndicatorTests
    {
        [TestMethod]
        public void DependencyLevel_1()
        {
            // Arrange
           
            var indicator4 = new Indicator("4", IndicatorType.CurrencyIndicator, "master", "4", "4", "f()", new List<IndicatorDependency>(), 0);

            var dependencies3 = new List<IndicatorDependency>
            {
                new IndicatorDependency("3", indicator4)
            };

            var indicator3 = new Indicator("3", IndicatorType.CurrencyIndicator, "master", "3", "3", "f()", dependencies3, 0);

            var dependencies2 = new List<IndicatorDependency>
            {
                new IndicatorDependency("2", indicator3)
            };

            var indicator2 = new Indicator("2", IndicatorType.CurrencyIndicator, "master", "2", "2", "f()", dependencies2, 0);

            var dependencies1 = new List<IndicatorDependency>
            {
                new IndicatorDependency("1", indicator2)
            };

            var indicator1 = new Indicator("1", IndicatorType.CurrencyIndicator, "master", "1", "1", "f()", dependencies1, 0);


            // Act
            var dependencyLevel = indicator1.DependencyLevel;

            // Assert
            Assert.AreEqual(3, dependencyLevel);
        }

    }
}
