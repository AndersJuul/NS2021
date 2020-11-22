using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Data.EntityAdapters;
using CleanArchitecture.IntegrationTests.Helpers;
using CleanArchitecture.Tests.Helpers;
using NUnit.Framework;

namespace CleanArchitecture.IntegrationTests.Infrastructure.Data.FileBased
{
    [TestFixture]
    public class ExcelRepositoryTests : BaseIntegrationTest
    {
        [Test]
        public async Task GetAll_ReturnsCounselors()
        {
            // Arrange
            var sut = new ExcelRepository(TestLogger.Create<ExcelRepository>(),
                new IEntityAdapter[] {new CounselorEntityAdapter(), new EventEntityAdapter(), new LocationEntityAdapter()});

            // Act
            var result = await sut.ListAsync<Counselor>();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetAll_ReturnsEvents()
        {
            // Arrange
            var sut = new ExcelRepository(TestLogger.Create<ExcelRepository>(),
                new IEntityAdapter[] { new CounselorEntityAdapter(), new EventEntityAdapter(), new LocationEntityAdapter() });

            // Act
            var result = await sut.ListAsync<Event>();

            // Assert
            Assert.AreEqual(41, result.Count);
        }

        [Test]
        public async Task GetAll_ReturnsLocations()
        {
            // Arrange
            var sut = new ExcelRepository(TestLogger.Create<ExcelRepository>(),
                new IEntityAdapter[] { new CounselorEntityAdapter(), new EventEntityAdapter(), new LocationEntityAdapter() });

            // Act
            var result = await sut.ListAsync<Location>();

            // Assert
            Assert.AreEqual(27, result.Count);
        }
    }
}