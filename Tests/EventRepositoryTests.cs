using System;
using System.Linq;
using Infrastructure.Data;
using Infrastructure.Data.FileBased;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Tests.Helpers;

namespace Tests
{
    [TestFixture]
    public class EventRepositoryTests: BaseIntegrationTest
    {
        public EventRepositoryTests()
        {
            TestContext.WriteLine("aTest started at: " + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"));
        }

        [Test]
        public void GetAll_ReturnsCounselors()
        {
            // Arrange
            var fileLocationOptions = new FileLocationOptions {Path = @".\\TestData"};
            var locationOptions = new OptionsWrapper<FileLocationOptions>(fileLocationOptions);
            var logger = TestLogger.Create<EventRepositoryExcel>();
            var sut = new EventRepositoryExcel(logger, locationOptions);

            // Act
            var result = sut.GetAll().ToArray();

            // Assert
            Assert.AreEqual(41, result.Length);
            Assert.AreEqual("mu",result[0].EventId);
        }
    }
}