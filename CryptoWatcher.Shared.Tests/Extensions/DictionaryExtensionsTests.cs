using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoWatcher.Shared.Extensions;

namespace CryptoWatcher.Shared.Tests.Extensions
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        [TestMethod]
        public void AsSplunkKeyValueString()
        {
            //Arrange
            var dictionary = new Dictionary<string, object>
            {
                {"ObjectId", "1"},
                { "ObjectName", "MyObject"}
            };

            //Act
            var str = dictionary.AsSplunkKeyValueString();

            //Assert
            Assert.AreEqual("ObjectId=1, ObjectName=MyObject", str);
        }
        [TestMethod]
        public void AsSplunkKeyValueString_WithSubDirectory()
        {
            //Arrange
            var dictionary = new Dictionary<string, object>
            {
                {"ObjectId", "1"},
                {"ObjectName", "MyObject"},
                {"SubClass", new Dictionary<string, object>
                {
                    {"SubClassId", 2},{"SubClassName", "MySubClass"}
                }}
            };

            //Act
            var str = dictionary.AsSplunkKeyValueString();

            //Assert
            Assert.AreEqual("ObjectId=1, ObjectName=MyObject, SubClass_SubClassId=2, SubClass_SubClassName=MySubClass", str);
        }
    }
}
