﻿using System;
using System.Linq;
using Infrastructure.Data.EntityAdapters;
using Infrastructure.Interfaces;
using ManyConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ns2020.App;
using Ns2020.App.Commands;
using NUnit.Framework;

namespace CleanArchitecture.IntegrationTests
{
    public class VerifyDependencies
    {
        [Test]
        public void ServiceProvider_GetRequiredService_CanConstructAllRequired()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var serviceCollection = Program.GetServiceCollection(configuration);
            foreach (var service in serviceCollection.OrderBy(x => x.ToString())) Console.WriteLine(service);
            Console.WriteLine();
            var services = serviceCollection.BuildServiceProvider();
            using var scope = services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var consoleCommands = serviceProvider
                .GetServices<ConsoleCommand>()
                .ToArray();
            Assert.True(consoleCommands.Any(x => x is MergeCommand));
            Assert.AreEqual(2, consoleCommands.Count());

            var adapters = serviceProvider.GetServices<IEntityAdapter>();
            Assert.AreEqual(5, adapters.Count());
        }
    }
}