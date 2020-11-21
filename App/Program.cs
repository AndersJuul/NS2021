using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Domain;
using Infrastructure.Data;
using Infrastructure.Data.FileBased;
using ManyConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ns2020.App
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("NS Planlægning 2020");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddLogging(c=>c.AddConsole())
                .AddScoped<ICounselorRepository, CounselorRepositoryExcel>();
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
