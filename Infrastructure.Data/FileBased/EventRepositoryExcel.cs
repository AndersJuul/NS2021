using System.Collections.Generic;
using System.IO;
using Domain.Abstractions;
using Domain.Model.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace Infrastructure.Data.FileBased
{
    public class EventRepositoryExcel : IEventRepository
    {
        private readonly ILogger<EventRepositoryExcel> _logger;
        private readonly IOptions<FileLocationOptions> _fileLocationOptions;

        public EventRepositoryExcel(ILogger<EventRepositoryExcel> logger,
            IOptions<FileLocationOptions> fileLocationOptions)
        {
            _logger = logger;
            _fileLocationOptions = fileLocationOptions;
        }

        public IEnumerable<Event> GetAll()
        {
            var fileName = Path.GetFullPath(Path.Combine(_fileLocationOptions.Value.Path, "Events.xlsx"));
            _logger.LogInformation("GetAll -- called, reading from " + fileName);

            using var package = new ExcelPackage(new FileInfo(fileName));

            var firstSheet = package.Workbook.Worksheets["Sheet1"];

            for (var row = 2; row < firstSheet.Cells.Rows; row++)
            {
                var eventId = firstSheet.Cells[$"A{row}"].Text;
                if (string.IsNullOrEmpty(eventId))
                    yield break;

                var description = firstSheet.Cells[$"B{row}"].Text;
                var remember = firstSheet.Cells[$"C{row}"].Text;
                var contactDaysBefore = firstSheet.Cells[$"D{row}"].Text;
                yield return new Event(eventId, description, remember, contactDaysBefore);
            }
        }
    }
}