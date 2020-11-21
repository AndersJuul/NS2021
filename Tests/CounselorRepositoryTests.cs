using System;
using System.Linq;
using Infrastructure.Data;
using Infrastructure.Data.FileBased;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CounselorRepositoryTests: BaseIntegrationTest
    {
        public CounselorRepositoryTests()
        {
            TestContext.WriteLine("aTest started at: " + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"));
        }

        [Test]
        public void GetAll_ReturnsCounselors()
        {
            // Arrange
            var fileLocationOptions = new FileLocationOptions {Path = @".\\TestData"};
            var locationOptions = new OptionsWrapper<FileLocationOptions>(fileLocationOptions);
            var logger = TestLogger.Create<CounselorRepositoryExcel>();
            var sut = new CounselorRepositoryExcel(logger, locationOptions);

            // Act
            var result = sut.GetAll().ToArray();

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("AJFAJ",result[0].Initials);
        }
    }
}