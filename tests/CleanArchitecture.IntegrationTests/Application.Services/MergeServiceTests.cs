using Application.Services;
using AutoFixture;
using CleanArchitecture.IntegrationTests.Helpers;
using CleanArchitecture.Tests.Helpers;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.EntityAdapters;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace CleanArchitecture.IntegrationTests.Application.Services
{
    [TestFixture]
    public class MergeServiceTests : BaseIntegrationTest
    {
        private readonly Fixture _fixture;

        public MergeServiceTests()
        {
            _fixture = new Fixture();
        }

       [Test]
        public void Run_XXX()
        {
            // Arrange
            var sut = GetSut();

            // Act
            sut.Merge();

            // Assert
            Assert.AreEqual(1,1);
        }

        private static MergeService GetSut()
        {
            var fileLocationOptions = new OptionsWrapper<FileLocationOptions>(new FileLocationOptions { WorkRoot = @"DefaultInput" });
            var excelRepository = new ExcelRepository(TestLogger.Create<ExcelRepository>(),
                new IEntityAdapter[]
                {
                    new CounselorEntityAdapter(), new EventEntityAdapter(), new LocationEntityAdapter(), new RequestEntityAdapter(),
                }, fileLocationOptions
            );
            return new MergeService(TestLogger.Create<MergeService>(), excelRepository);
        }
    }
}