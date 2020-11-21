using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Domain;
using Domain.Abstractions;
using Infrastructure.Data;
using Infrastructure.Data.FileBased;
using ManyConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace Ns2020.App
{
    class Program
    {
        static int Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Console.WriteLine("NS Planlægning 2020");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(c=>c.AddSerilog());
            serviceCollection.AddScoped<ICounselorRepository, CounselorRepositoryExcel>();
            serviceCollection.AddScoped<IEventRepository, EventRepositoryExcel>();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddOptions();
            serviceCollection.Configure<FileLocationOptions>(configuration.GetSection("FileLocation"));

            Assembly.GetAssembly(typeof(MergeCommand))?
                .GetTypesAssignableFrom<ConsoleCommand>()
                .ForEach(t => { serviceCollection.AddScoped(typeof(ConsoleCommand), t); });

            var serviceProvider = serviceCollection
                .BuildServiceProvider();

            // locate any commands in the assembly (or use an IoC container, or whatever source)
            var commands = GetCommands(serviceProvider);

            // then run them.
            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }

        public static IEnumerable<ConsoleCommand> GetCommands(ServiceProvider serviceProvider)
        {
            var consoleCommands = serviceProvider.GetServices<ConsoleCommand>();
            return consoleCommands;
        }
    }
}
