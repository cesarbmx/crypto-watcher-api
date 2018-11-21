using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoWatcher.Shared.Extensions;

namespace CryptoWatcher.Shared.Tests.Extensions
{
    [TestClass]
    public class UrlExtensionsTests
    {
        [TestMethod]
        public void IsUrl_ReturnTrueForValidUrl()
        {
            //Arrange
            const string url = "https://wwww.google.ca";

            //Act
            var isUrl = url.IsUrl();

            //Assert
            Assert.IsTrue(isUrl);
        }

        [TestMethod]
        public void IsUrl_ReturnFalseForInvalidValidUrl()
        {
            //Arrange
            const string url = "htt://wwww.google.ca";

            //Act
            var isUrl = url.IsUrl();

            //Assert
            Assert.IsFalse(isUrl);
        }
    }
}
