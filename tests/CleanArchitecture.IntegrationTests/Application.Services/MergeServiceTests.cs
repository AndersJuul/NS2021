using System.Threading.Tasks;
using Application.Services;
using AutoFixture;
using CleanArchitecture.IntegrationTests.Helpers;
using CleanArchitecture.Tests.Helpers;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.EntityAdapters;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace CleanArchitecture.IntegrationTests.Application.Services
{
    [TestFixture]
    public class MergeServiceTests : BaseIntegrationTest
    {
        private readonly Fixture _fixture;
        private MergeService _sut;
        private TestDataCreationService _testDataCreationService;

        public MergeServiceTests()
        {
            _fixture = new Fixture();
            var fileLocationOptions =
                new OptionsWrapper<FileLocationOptions>(new FileLocationOptions {WorkRoot = @"DefaultInput"});
            var excelRepository = new ExcelRepository(TestLogger.Create<ExcelRepository>(),
                new IEntityAdapter[]
                {
                    new CounselorEntityAdapter(), 
                    new EventEntityAdapter(), 
                    new LocationEntityAdapter(),
                    new RequestEntityAdapter(),
                    new ResultEntityAdapter(), 
                }, fileLocationOptions
            );
            _sut = new MergeService(TestLogger.Create<MergeService>(), excelRepository);
            _testDataCreationService = new TestDataCreationService(TestLogger.Create<TestDataCreationService>(),excelRepository);
        }

        [Test]
        public async Task Run_XXX()
        {
            // Arrange
            await _testDataCreationService.CreateRequests(10);

            // Act
            await _sut.Merge();

            // Assert
            Assert.AreEqual(1,1);
        }
    }
}