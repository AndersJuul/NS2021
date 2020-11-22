using System;
using System.Linq;
using ManyConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ns2020.App;
using Ns2020.App.Commands;
using NUnit.Framework;

namespace Tests
{
    public class VerifyDependencies
    {
        [Test]
        public void ServiceProvider_GetRequiredService_CanConstructAllControllers()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var serviceCollection = Program.GetServiceCollection(configuration);
            foreach (var service in serviceCollection.OrderBy(x => x.ToString())) Console.WriteLine(service);
            var services = serviceCollection.BuildServiceProvider();

            using var scope = services.CreateScope();
            var consoleCommands = scope
                .ServiceProvider
                .GetServices<ConsoleCommand>()
                .ToArray();
            Assert.True(consoleCommands.Any(x => x is MergeCommand));
            Assert.AreEqual(1, consoleCommands.Count());
        }
    }
}