using System.Collections.Generic;
using System.IO;
using Domain;
using Domain.Model.Entities;
using Domain.Model.ValueObjects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace Infrastructure.Data.FileBased
{
    public class EventRepositoryExcel : IEventRepository
    {
        private readonly ILogger<EventRepositoryExcel> _logger;
        private readonly string _fileName;

        public EventRepositoryExcel(ILogger<EventRepositoryExcel> logger, IOptions<FileLocationOptions> fileLocationOptions)
        {
            _logger = logger;
            _fileName =Path.GetFullPath(Path.Combine(fileLocationOptions.Value.Path, "Events.xlsx"));
        }

        public IEnumerable<Event> GetAll()
        {
            _logger.LogInformation("GetAll -- called, reading from "+_fileName);

            using var package = new ExcelPackage(new FileInfo(_fileName));
            
            var firstSheet = package.Workbook.Worksheets["Sheet1"];
            
            for (var row = 2; row < firstSheet.Cells.Rows; row++)
            {
                var eventId = firstSheet.Cells[$"A{row}"].Text;
                if(string.IsNullOrEmpty( eventId))
                    yield break;

                var description = firstSheet.Cells[$"B{row}"].Text;
                var remember = firstSheet.Cells[$"C{row}"].Text;
                var contactDaysBefore = firstSheet.Cells[$"D{row}"].Text;
                yield return new Event(eventId, description,remember, contactDaysBefore);
            }
        }
    }
}
