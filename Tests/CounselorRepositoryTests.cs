using System.Linq;
using Infrastructure.Data;
using Infrastructure.Data.FileBased;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CounselorRepositoryTests
    {
        [Test]
        public void GetAll_ReturnsCounsolers()
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