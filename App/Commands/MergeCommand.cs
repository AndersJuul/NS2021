﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions;
using Infrastructure.Data;
using ManyConsole;
using Microsoft.Extensions.Options;
using Mono.Options;

namespace Ns2020.App
{
    public class MergeCommand : ConsoleCommand
    {
        private readonly ICounselorRepository _counselorRepository;

        //public string Argument1;
        //public string Argument2;
        //public string OptionalArgument1;
        //public string OptionalArgument2;
        public bool BooleanOption;
        public List<string> OptionalArgumentList = new List<string>();
        private readonly IEventRepository _eventRepository;

        public MergeCommand(ICounselorRepository counselorRepository, IEventRepository eventRepository, IOptions<FileLocationOptions> fileLocationOptions)
        {
            _counselorRepository = counselorRepository;
            _eventRepository = eventRepository;
            IsCommand("Flet", "Fletter Vejledere, Arrangementer og Steder til Resultatfil");

            //HasOption("b|booleanOption", "Boolean flag option", b => BooleanOption = true);

            //  Setting .Options directly is the old way to do this, you may prefer to call the helper
            //  method HasOption/HasRequiredOption.
            //Options = new OptionSet
            //{
            //    {"l|list=", "Values to add to list", v => OptionalArgumentList.Add(v)}
            //    //{"r|requiredArguments=", "Optional string argument requiring a value be specified afterwards", s => OptionalArgument1 = s},
            //    //{"o|optionalArgument:", "Optional String argument which is null if no value follow is specified", s => OptionalArgument2 = s ?? "<no argument specified>"}
            //};

            //HasRequiredOption("requiredOption=", "Required string argument also requiring a value.", s => { });
            HasOption("path=", "(Valgfri) Angivelse af sti for .xlxs-filer", s => { fileLocationOptions.Value.Path = s; });

            //HasAdditionalArguments(2, "<Argument1> <Argument2>");
        }

        public override int Run(string[] remainingArguments)
        {
            //Argument1 = remainingArguments[0];
            //Argument2 = remainingArguments[1];

            //Console.WriteLine(@"Called Example command - Argument1 = ""{0}"" Argument2 = ""{1}"" BooleanOption: {2}", Argument1, Argument2, BooleanOption);

            OptionalArgumentList.ForEach(item =>
                Console.WriteLine(@"List Item {0} = ""{1}""", OptionalArgumentList.IndexOf(item), item));

            if (BooleanOption) throw new Exception("Throwing unhandled exception because BooleanOption is true");

            var counselors = _counselorRepository.GetAll().ToArray();
            Console.WriteLine(counselors.Length);

            var events = _eventRepository.GetAll().ToArray();
            Console.WriteLine(events.Length);

            return 0;
        }
    }
}