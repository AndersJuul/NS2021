using System.Collections.Generic;
using System.IO;
using Domain.Abstractions;
using Domain.Model.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace Infrastructure.Data.FileBased
{
    public class LocationRepositoryExcel : ILocationRepository
    {
            private readonly ILogger<LocationRepositoryExcel> _logger;
            private readonly IOptions<FileLocationOptions> _fileLocationOptions;

            public LocationRepositoryExcel(ILogger<LocationRepositoryExcel> logger,
                IOptions<FileLocationOptions> fileLocationOptions)
            {
                _logger = logger;
                _fileLocationOptions = fileLocationOptions;
            }

            public IEnumerable<Location> GetAll()
            {
                var fileName = Path.GetFullPath(Path.Combine(_fileLocationOptions.Value.Path, "Locations.xlsx"));
                _logger.LogInformation("GetAll -- called, reading from " + fileName);

                using var package = new ExcelPackage(new FileInfo(fileName));

                var firstSheet = package.Workbook.Worksheets["Sheet1"];

                for (var row = 2; row < firstSheet.Cells.Rows; row++)
                {
                    var id = firstSheet.Cells[$"A{row}"].Text;
                    if (string.IsNullOrEmpty(id))
                        yield break;

                    var description = firstSheet.Cells[$"B{row}"].Text;
                    yield return new Location(id, description);
                }
            }
    }
    }