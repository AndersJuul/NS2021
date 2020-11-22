using System;
using System.Collections.Generic;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ns2020.App.Commands
{
    public class MergeCommand : NsConsoleCommand
    {
        private readonly ILogger<MergeCommand> _logger;
        private readonly IRepository _repository;

        //public string Argument1;
        //public string Argument2;
        //public string OptionalArgument1;
        //public string OptionalArgument2;
        public bool BooleanOption;
        public List<string> OptionalArgumentList = new List<string>();

        public MergeCommand(IOptions<FileLocationOptions> fileLocationOptions, ILogger<MergeCommand> logger,
            IRepository repository):base(fileLocationOptions)
        {
            _logger = logger;
            _repository = repository;
            IsCommand("Flet", "Fletter Vejledere, Arrangementer og Steder til Resultatfil");

        }

        public override int Run(string[] remainingArguments)
        {
            OptionalArgumentList.ForEach(item =>
                Console.WriteLine(@"List Item {0} = ""{1}""", OptionalArgumentList.IndexOf(item), item));

            if (BooleanOption) throw new Exception("Throwing unhandled exception because BooleanOption is true");

            var counselors = _repository.ListAsync<Counselor>().Result;
            _logger.LogInformation("Vejledere: " + counselors.Count);

            var events = _repository.ListAsync<Event>().Result;
            _logger.LogInformation("Arrangementer: " + events.Count);

            var locations = _repository.ListAsync<Location>().Result;
            _logger.LogInformation("Steder: " + locations.Count);

            var requests = _repository.ListAsync<Request>().Result;
            _logger.LogInformation("Ønsker: " + requests.Count);

            //_mergeService.Merge();

            return 0;
        }
    }
}