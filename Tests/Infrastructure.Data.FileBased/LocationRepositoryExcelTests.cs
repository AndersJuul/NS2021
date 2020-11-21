using System.Linq;
using Infrastructure.Data;
using Infrastructure.Data.FileBased;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Tests.Helpers;

namespace Tests.Infrastructure.Data.FileBased
{
    [TestFixture]
    public class LocationRepositoryExcelTests : BaseIntegrationTest
    {
        [Test]
        public void GetAll_ReturnsLocations()
        {
            // Arrange
            var fileLocationOptions = new FileLocationOptions {Path = @".\\TestData"};
            var locationOptions = new OptionsWrapper<FileLocationOptions>(fileLocationOptions);
            var logger = TestLogger.Create<LocationRepositoryExcel>();
            var sut = new LocationRepositoryExcel(logger, locationOptions);

            // Act
            var result = sut.GetAll().ToArray();

            // Assert
            Assert.AreEqual(27, result.Length);
            Assert.AreEqual("bisko", result[0].Id);
        }
    }
}