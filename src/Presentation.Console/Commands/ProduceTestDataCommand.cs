using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Kernel;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.SharedKernel.Interfaces;
using ManyConsole;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ns2020.App.Commands
{
    public class ProduceTestDataCommand : ConsoleCommand
    {
        private readonly ILogger<MergeCommand> _logger;
        private readonly IRepository _repository;

        private readonly List<string> _optionalArgumentList = new List<string>();
        private Fixture _fixture;

        public ProduceTestDataCommand(IOptions<FileLocationOptions> fileLocationOptions, ILogger<MergeCommand> logger,
            IRepository repository)
        {
            _fixture=new Fixture();
            _logger = logger;
            _repository = repository;
            IsCommand("ProduceTestData", "Producerer en dummy Request-fil");

            HasOption("path=", "(Valgfri) Angivelse af sti for .xlxs-filer",
                s => { fileLocationOptions.Value.Path = s; });
        }

        public override int Run(string[] remainingArguments)
        {
            _optionalArgumentList.ForEach(item =>
                Console.WriteLine(@"List Item {0} = ""{1}""", _optionalArgumentList.IndexOf(item), item));

            var counselors = _repository.ListAsync<Counselor>().Result;
            _logger.LogInformation("Vejledere: " + counselors.Count);

            var events = _repository.ListAsync<Event>().Result;
            _logger.LogInformation("Arrangementer: " + events.Count);

            var locations = _repository.ListAsync<Location>().Result;
            _logger.LogInformation("Steder: " + locations.Count);


            for (int i = 0; i < 50; i++)
            {
                var request = _fixture
                    .Build<Request>()
                    .Without(x => x.Events)
                    .With(x=>x.Id, DateTime.Now.ToString("yyyy-MM-dd.HH.mm.ss.fff"))
                    .With(x=>x.ContactName, GetContactName())
                    .With(x=>x.ContactPhone, GetContactPhone())
                    .Create();
                var result = _repository.AddAsync(request).Result;
            }

            var requestsAfter = _repository.ListAsync<Request>().Result;
            _logger.LogInformation("Ønsker: " + requestsAfter.Count);

            return 0;
        }

        private string GetContactPhone()
        {
            var r = new Random();
            var result = "";
            while (result.Length < 8)
            {
                result += r.Next(1, 10);
            }

            return result;
        }

        private string GetContactName()
        {
            var firstnames = new[] { "Ole", "Kurt", "Jonna", "Claus", "Signe", "Thomas", "Andy", "Gitte", "Kasper", "Christian", "Isabella", "Victoria", "Henriette", "Jesper" };
            var lastnames = new[] { "Olsen", "Clausen", "Thomasson", "Kaspersen", "Christiansen", "Jespersen", "Jensen","Juul", "Juel", "Hansen" };
            var r=new Random();
            return firstnames[r.Next(firstnames.Length)] + " " + lastnames[r.Next(lastnames.Length)];
        }
    }
}