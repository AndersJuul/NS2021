using System;
using System.Collections.Generic;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Domain.Model.Entities;
using Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ns2020.App.Commands
{
    public class MergeCommand : NsConsoleCommand
    {
        private readonly ILogger<MergeCommand> _logger;
        private readonly IRepository _repository;

        private readonly List<string> _optionalArgumentList = new List<string>();
        private readonly IMergeService _mergeService;

        public MergeCommand(ILogger<MergeCommand> logger,
            IOptions<FileLocationOptions> fileLocationOptions,
            IRepository repository, IMergeService mergeService):base(fileLocationOptions)
        {
            _logger = logger;
            _repository = repository;
            _mergeService = mergeService;
            IsCommand("Flet", "Fletter Vejledere, Arrangementer og Steder til Resultatfil");

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

            var requests = _repository.ListAsync<Request>().Result;
            _logger.LogInformation("Ønsker: " + requests.Count);

            _mergeService.Merge();

            return 0;
        }
    }
}