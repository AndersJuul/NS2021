using Application.Services;
using AutoFixture;
using CleanArchitecture.IntegrationTests.Helpers;
using CleanArchitecture.Tests.Helpers;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.EntityAdapters;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Ns2020.App.Commands;
using NUnit.Framework;

namespace CleanArchitecture.IntegrationTests.Presentation.Console
{
    [TestFixture]
    public class MergeCommandTests : BaseIntegrationTest
    {
        private readonly Fixture _fixture;

        public MergeCommandTests()
        {
            _fixture = new Fixture();
        }

        private static MergeCommand GetSut()
        {
            var fileLocationOptions =
                new OptionsWrapper<FileLocationOptions>(new FileLocationOptions {WorkRoot = @"DefaultInput"});
            var excelRepository = new ExcelRepository(TestLogger.Create<ExcelRepository>(),
                new IEntityAdapter[]
                {
                    new CounselorEntityAdapter(), new EventEntityAdapter(), new LocationEntityAdapter(),
                    new RequestEntityAdapter()
                }, fileLocationOptions
            );
            var mergeService = new MergeService(TestLogger.Create<MergeService>(),excelRepository);
            return new MergeCommand(TestLogger.Create<MergeCommand>(),
                fileLocationOptions,
                excelRepository, 
                mergeService);
        }

        [Test]
        public void Run_XXX()
        {
            // Arrange
            var sut = GetSut();

            // Act
            var result = sut.Run(new string[] { });

            // Assert
            Assert.AreEqual(1, 1);
        }
    }
}