using System;
using System.IO;
using CleanArchitecture.Infrastructure;
using ManyConsole;
using Microsoft.Extensions.Options;

namespace Ns2020.App.Commands
{
    public abstract class NsConsoleCommand:ConsoleCommand
    {
        private FileLocationOptions _fileLocationOptions;

        protected NsConsoleCommand(IOptions<FileLocationOptions> fileLocationOptions)
        {
            HasOption("path=", "(Valgfri) Angivelse af sti for .xlxs-filer",
                s => { fileLocationOptions.Value.Path = s; });
            HasOption("workroot=", "(Valgfri) Angivelse af rodfolder for filer",
                s => { fileLocationOptions.Value.WorkRoot = s; });

            _fileLocationOptions = fileLocationOptions.Value;
        }
    }
}