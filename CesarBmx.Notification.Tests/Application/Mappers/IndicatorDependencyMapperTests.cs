using AutoMapper;
using CesarBmx.Notification.Application.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CesarBmx.Notification.Tests.Application.Mappers
{
    [TestClass]
    public class IndicatorDependencyMapperTests
    {
        [TestMethod]
        public void Test_IndicatorDependencyMapper()
        {
            // Arrange
            IMapper config = new MapperConfiguration(cfg => { cfg.AddProfile<IndicatorDependencyMapper>(); }).CreateMapper();
            
            // Assert
            config.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
