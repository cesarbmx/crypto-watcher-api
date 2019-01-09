using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoWatcher.Shared.Extensions;

namespace CryptoWatcher.Shared.Tests.ExtensionsTests
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
                {"ObjectName", "MyObject"}
            };

            //Act
            var str = dictionary.AsSplunkKeyValueString();

            //Assert
            Assert.AreEqual("ObjectId=1, ObjectName=MyObject", str);
        }
        [TestMethod]
        public void AsSplunkKeyValueString_WithNull()
        {
            //Arrange
            var dictionary = new Dictionary<string, object>
            {
                {"ObjectId", "1"},
                {"SubClass",  null}
            };

            //Act
            var str = dictionary.AsSplunkKeyValueString();

            //Assert
            Assert.AreEqual("ObjectId=1, SubClass=null", str);
        }
        [TestMethod]
        public void AsSplunkKeyValueString_WithDate()
        {
            //Arrange
            var dictionary = new Dictionary<string, object>
            {
                {"ObjectId", "1"},
                {"SubClass",  DateTime.MinValue}
            };

            //Act
            var str = dictionary.AsSplunkKeyValueString();

            //Assert
            Assert.AreEqual($"ObjectId=1, SubClass=\"{DateTime.MinValue}\"", str);
        }
        [TestMethod]
        public void AsSplunkKeyValueString_WithSpaces()
        {
            //Arrange
            var dictionary = new Dictionary<string, object>
            {
                {"ObjectId", "1"},
                {"ObjectName", "MyObject"},
                {"ObjectDescription", "Blah blah blah"}
            };

            //Act
            var str = dictionary.AsSplunkKeyValueString();

            //Assert
            Assert.AreEqual("ObjectId=1, ObjectName=MyObject, ObjectDescription=\"{...}\"", str);
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
