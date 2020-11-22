using System.Threading.Tasks;
using AutoFixture;
using CleanArchitecture.IntegrationTests.Helpers;
using CleanArchitecture.Tests.Helpers;
using Domain.Model.Entities;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.EntityAdapters;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
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
        public async Task AddAsync_ReturnsRequests()
        {
            // Arrange
            var request = _fixture.Build<Request>().Without(x=>x.Events).Create();
            var sut = GetSut();
            var before = (await sut.ListAsync<Request>()).Count;

            // Act
            var result = await sut.AddAsync(request);

            // Assert
            var after = (await sut.ListAsync<Request>()).Count;
            Assert.AreEqual(before+1,after);
        }

        private static ExcelRepository GetSut()
        {
            return new ExcelRepository(
                TestLogger.Create<ExcelRepository>(),
                new IEntityAdapter[]
                {
                    new CounselorEntityAdapter(), new EventEntityAdapter(), new LocationEntityAdapter(), new RequestEntityAdapter(),
                },
                new OptionsWrapper<FileLocationOptions>(new FileLocationOptions{WorkRoot= @"DefaultInput"}));
        }
    }
}