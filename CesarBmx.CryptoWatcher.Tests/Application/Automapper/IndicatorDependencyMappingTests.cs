using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CesarBmx.CryptoWatcher.Tests.Application.Automapper
{
    [TestClass]
    public class IndicatorDependencyMappingTests
    {
        [TestMethod]
        public void Test_IndicatorDependencyMapping()
        {
            // Arrange
            IMapper config = new MapperConfiguration(cfg => { cfg.AddProfile<IndicatorDependencyMapping>(); }).CreateMapper();
            
            // Assert
            config.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
