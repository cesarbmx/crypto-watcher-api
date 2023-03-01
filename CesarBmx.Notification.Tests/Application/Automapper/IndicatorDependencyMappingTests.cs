using AutoMapper;
using CesarBmx.Notification.Application.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CesarBmx.Notification.Tests.Application.Automapper
{
    [TestClass]
    public class IndicatorDependencyMappingTests
    {
        [TestMethod]
        public void Test_IndicatorDependencyMapping()
        {
            // Arrange
            IMapper config = new MapperConfiguration(cfg => { cfg.AddProfile<IndicatorDependencyMapper>(); }).CreateMapper();
            
            // Assert
            config.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
