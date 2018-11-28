using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoWatcher.Shared.Extensions;

namespace CryptoWatcher.Shared.Tests.Extensions
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void AsDictionary()
        {
            //Arrange
            var obj = new { ObjectId = 1, ObjectName = "MyObject" };

            //Act
            var dictionary = obj.AsDictionary();

            //Assert
            Assert.IsTrue(dictionary.Count == 2);
            Assert.AreEqual(1, dictionary["ObjectId"]);
            Assert.AreEqual("MyObject", dictionary["ObjectName"]);
        }
        [TestMethod]
        public void AsDictionary_WithSubClass()
        {
            //Arrange
            var subClass = new { SubClassId = 2, SubClassName = "MySubClass" };
            var obj = new { ObjectId = 1, ObjectName = "MyObject", SubClass = subClass };

            //Act
            var dictionary = obj.AsDictionary();

            //Assert
            Assert.IsTrue(dictionary.Count == 3);
            Assert.AreEqual(1, dictionary["ObjectId"]);
            Assert.AreEqual("MyObject", dictionary["ObjectName"]);
            Assert.IsTrue(dictionary["SubClass"] is Dictionary<string,object>);
            Assert.AreEqual(2, (dictionary["SubClass"] as Dictionary<string, object>)["SubClassId"]);
            Assert.AreEqual("MySubClass", (dictionary["SubClass"] as Dictionary<string, object>)["SubClassName"]);
        }
    }
}
