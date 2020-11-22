using System;
using System.Collections.Generic;
using Application.Interfaces;
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
        private readonly ITestDataCreationService _testDataCreationService;

        public ProduceTestDataCommand(ILogger<ProduceTestDataCommand> logger,
            IOptions<FileLocationOptions> fileLocationOptions,
            IRepository repository, ITestDataCreationService testDataCreationService):base(fileLocationOptions)
        {
            _logger = logger;
            _repository = repository;
            _testDataCreationService = testDataCreationService;
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

            _testDataCreationService.CreateRequests(50);

            var requestsAfter = _repository.ListAsync<Request>().Result;
            _logger.LogInformation("Ønsker: " + requestsAfter.Count);

            return 0;
        }
    }
}