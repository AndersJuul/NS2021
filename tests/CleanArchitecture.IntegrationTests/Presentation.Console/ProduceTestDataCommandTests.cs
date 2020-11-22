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
    public class ProduceTestDataCommandTests : BaseIntegrationTest
    {
        private readonly Fixture _fixture;

        public ProduceTestDataCommandTests()
        {
            _fixture = new Fixture();
        }

        private static ProduceTestDataCommand GetSut()
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
            var testDataCreationService =
                new TestDataCreationService(TestLogger.Create<TestDataCreationService>(), excelRepository);
            return new ProduceTestDataCommand(TestLogger.Create<ProduceTestDataCommand>(),
                fileLocationOptions, excelRepository, testDataCreationService);
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