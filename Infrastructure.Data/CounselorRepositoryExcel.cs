using System;
using System.Collections.Generic;
using System.IO;
using Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace Infrastructure.Data
{
    public class CounselorRepositoryExcel: ICounselorRepository
    {
        private readonly ILogger<CounselorRepositoryExcel> _logger;
        private FileLocationOptions _fileLocationOptions;
        private string _fileName;

        public CounselorRepositoryExcel(ILogger<CounselorRepositoryExcel> logger, IOptions<FileLocationOptions> fileLocationOptions)
        {
            _logger = logger;
            _fileLocationOptions = fileLocationOptions.Value;
            _fileName = Path.Combine(_fileLocationOptions.Path, "Counselors.xlsx");
        }

        public IEnumerable<Counselor> GetAll()
        {
            _logger.LogInformation("GetAll -- called");

            using (var package = new ExcelPackage(new FileInfo(_fileName)))
            {
                var firstSheet = package.Workbook.Worksheets["Sheet1"];
                for (int row = 2; row < firstSheet.Cells.Rows; row++)
                {
                    var initials = firstSheet.Cells[$"A{row}"].Text;
                    if(string.IsNullOrEmpty( initials))
                        yield break;
                    yield return new Counselor(initials);
                }
            }
        }
    }
}
