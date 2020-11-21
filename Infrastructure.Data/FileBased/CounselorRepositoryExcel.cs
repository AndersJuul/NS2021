using System.Collections.Generic;
using System.IO;
using Domain;
using Domain.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace Infrastructure.Data.FileBased
{
    public class CounselorRepositoryExcel: ICounselorRepository
    {
        private readonly ILogger<CounselorRepositoryExcel> _logger;
        private readonly string _fileName;

        public CounselorRepositoryExcel(ILogger<CounselorRepositoryExcel> logger, IOptions<FileLocationOptions> fileLocationOptions)
        {
            _logger = logger;
            _fileName =Path.GetFullPath(Path.Combine(fileLocationOptions.Value.Path, "Counselors.xlsx"));
        }

        public IEnumerable<Counselor> GetAll()
        {
            _logger.LogInformation("GetAll -- called, reading from "+_fileName);

            using var package = new ExcelPackage(new FileInfo(_fileName));
            
            var firstSheet = package.Workbook.Worksheets["Sheet1"];
            
            for (int row = 2; row < firstSheet.Cells.Rows; row++)
            {
                var initials = firstSheet.Cells[$"A{row}"].Text;
                if(string.IsNullOrEmpty( initials))
                    yield break;

                yield return new Counselor(initials, firstSheet.Cells[$"B{row}"].Text, firstSheet.Cells[$"C{row}"].Text, firstSheet.Cells[$"D{row}"].Text);
            }
        }
    }
}
