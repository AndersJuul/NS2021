using System;
using System.Collections.Generic;
using AutoFixture;
using Domain.Interfaces;
using Domain.Model.Entities;
using Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ns2020.App.Commands
{
    public class ProduceTestDataCommand : NsConsoleCommand
    {
        private readonly ILogger<ProduceTestDataCommand> _logger;
        private readonly IRepository _repository;

        private readonly List<string> _optionalArgumentList = new List<string>();
        private readonly Fixture _fixture;

        public ProduceTestDataCommand(ILogger<ProduceTestDataCommand> logger,
            IOptions<FileLocationOptions> fileLocationOptions,
            IRepository repository):base(fileLocationOptions)
        {
            _fixture=new Fixture();
            _logger = logger;
            _repository = repository;
            IsCommand("ProduceTestData", "Producerer en dummy Request-fil");
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