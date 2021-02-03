using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Tests.Domain.Builders
{
    [TestClass]
    public class BuildPeriod
    {
        [TestMethod]
        public void Test_OneDay()
        {
            // Arrange
            var time = new DateTime(2021, 2,5,0,0,0);

            // Act
            var period = LineBuilder.BuildPeriod(time);

            // Assert
            Assert.AreEqual(Period.ONE_DAY, period);
        }

        [TestMethod]
        public void Test_OneHour()
        {
            // Arrange
            var time = new DateTime(2021, 2, 5, 10, 0, 0);

            // Act
            var period = LineBuilder.BuildPeriod(time);

            // Assert
            Assert.AreEqual(Period.ONE_HOUR, period);
        }
        
        [TestMethod]
        public void Test_FifteenMinutes()
        {
            // Arrange
            var time = new DateTime(2021, 2, 5, 10, 30, 0);

            // Act
            var period = LineBuilder.BuildPeriod(time);

            // Assert
            Assert.AreEqual(Period.FIFTEEN_MINUTES, period);
        }

        [TestMethod]
        public void Test_FiveMinutes()
        {
            // Arrange
            var time = new DateTime(2021, 2, 5, 10, 10, 0);

            // Act
            var period = LineBuilder.BuildPeriod(time);

            // Assert
            Assert.AreEqual(Period.FIVE_MINUTES, period);
        }

        [TestMethod]
        public void Test_OneMinute()
        {
            // Arrange
            var time = new DateTime(2021, 2, 5, 10, 12, 0);

            // Act
            var period = LineBuilder.BuildPeriod(time);

            // Assert
            Assert.AreEqual(Period.ONE_MINUTE, period);
        }
    }
}
