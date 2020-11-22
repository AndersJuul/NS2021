using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
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
        private readonly Fixture _fixture;

        public ExcelRepositoryTests()
        {
            _fixture = new Fixture();
            //_fixture.Customizations.Add(
            //    new TypeRelay(
            //        typeof(SharedKernel.BaseDomainEvent),
            //        typeof(Request)));
        }

        [Test]
        public async Task ListAsync_ReturnsCounselors()
        {
            // Arrange
            var sut = GetSut();

            // Act
            var result = await sut.ListAsync<Counselor>();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task ListAsync_ReturnsEvents()
        {
            // Arrange
            var sut = GetSut();

            // Act
            var result = await sut.ListAsync<Event>();

            // Assert
            Assert.AreEqual(41, result.Count);
        }

        [Test]
        public async Task ListAsync_ReturnsLocations()
        {
            // Arrange
            var sut = GetSut();

            // Act
            var result = await sut.ListAsync<Location>();

            // Assert
            Assert.AreEqual(27, result.Count);
        }

        [Test]
        public async Task ListAsync_ReturnsRequests()
        {
            // Arrange
            var sut = GetSut();

            // Act
            var result = await sut.ListAsync<Request>();

            // Assert
            Assert.AreEqual(217, result.Count);
        }

        [Test]
        public async Task AddAsync_ReturnsRequests()
        {
            // Arrange
            var request = _fixture.Build<Request>().Without(x=>x.Events).Create();
            var sut = GetSut();

            // Act
            var result = await sut.AddAsync(request);

            // Assert
            //TODO
        }

        private static ExcelRepository GetSut()
        {
            return new ExcelRepository(TestLogger.Create<ExcelRepository>(),
                new IEntityAdapter[] { new CounselorEntityAdapter(), new EventEntityAdapter(), new LocationEntityAdapter(), new RequestEntityAdapter(),  });
        }
    }
}