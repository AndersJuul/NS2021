//using System.Collections.Generic;
//using System.IO;
//using CleanArchitecture.Core.Entities;
//using Domain.Model.ValueObjects;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using OfficeOpenXml;

//namespace Infrastructure.Data.FileBased
//{
//    public class CounselorRepositoryExcel : ICounselorRepository
//    {
//        private readonly IOptions<FileLocationOptions> _fileLocationOptions;
//        private readonly ILogger<CounselorRepositoryExcel> _logger;

//        public CounselorRepositoryExcel(ILogger<CounselorRepositoryExcel> logger,
//            IOptions<FileLocationOptions> fileLocationOptions)
//        {
//            _logger = logger;
//            _fileLocationOptions = fileLocationOptions;
//        }

//        public IEnumerable<Counselor> GetAll()
//        {
//            var fileName = Path.GetFullPath(Path.Combine(_fileLocationOptions.Value.Path, "Counselors.xlsx"));
//            _logger.LogInformation("GetAll -- called, reading from " + fileName);

//            using var package = new ExcelPackage(new FileInfo(fileName));

//            var firstSheet = package.Workbook.Worksheets["Sheet1"];

//            for (var row = 2; row < firstSheet.Cells.Rows; row++)
//            {
//                var initials = firstSheet.Cells[$"A{row}"].Text;
//                if (string.IsNullOrEmpty(initials))
//                    yield break;

//                var phoneNumber = new PhoneNumber(firstSheet.Cells[$"C{row}"].Text);
//                yield return new Counselor(initials, firstSheet.Cells[$"B{row}"].Text, phoneNumber,
//                    firstSheet.Cells[$"D{row}"].Text);
//            }
//        }
//    }
//}