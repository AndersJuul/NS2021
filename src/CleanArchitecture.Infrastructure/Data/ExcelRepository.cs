using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using CleanArchitecture.Infrastructure.Data.EntityAdapters;
using CleanArchitecture.SharedKernel;
using CleanArchitecture.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace CleanArchitecture.Infrastructure.Data
{
    public class ExcelRepository : IRepository
    {
        private readonly IEnumerable<IEntityAdapter> _converters;
        private readonly FileLocationOptions _fileLocationOptions;
        private readonly ILogger<ExcelRepository> _logger;

        public ExcelRepository(ILogger<ExcelRepository> logger, IEnumerable<IEntityAdapter> converters,
            IOptions<FileLocationOptions> fileLocationOptions)
        {
            _logger = logger;
            _converters = converters;
            _fileLocationOptions = fileLocationOptions.Value;
        }

        public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity, IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity, IAggregateRoot
        {
            var type = typeof(T);
            var converter = _converters.SingleOrDefault(x => x.CanHandle(type));
            if (converter == null)
                throw new ArgumentException("No converter found for " + type.FullName);

            var fileName = Path.GetFullPath(Path.Combine(_fileLocationOptions.Path, converter.GetFileName()));
            _logger.LogInformation("GetAll -- called, reading from " + fileName);

            using var package = new ExcelPackage(new FileInfo(fileName));
            if (!package.File.Exists)
                throw new ArgumentException("File not found: " + fileName);
            var firstSheet = package.Workbook.Worksheets[0];

            var result = new List<T>();

            for (var row = 2; row < firstSheet.Cells.Rows; row++)
            {
                var id = firstSheet.Cells[$"A{row}"].Text;
                if (string.IsNullOrEmpty(id))
                    break;

                var rowValues = new List<string> {id};
                for (var column = 2; column < firstSheet.Cells.Columns; column++)
                {
                    var cellAddress = ((char) (64 + column)).ToString() + row;
                    var cellValue = firstSheet.Cells[cellAddress].Text;
                    if (string.IsNullOrEmpty(cellValue))
                        break;
                    rowValues.Add(cellValue);
                }

                result.Add(converter.GetEntity(rowValues) as T);
            }

            return result;
        }

        public Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseEntity, IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        {
            var type = typeof(T);
            var converter = _converters.SingleOrDefault(x => x.CanHandle(type));
            if (converter == null)
                throw new ArgumentException("No converter found for " + type.FullName);

            var fileName = Path.GetFullPath(Path.Combine(_fileLocationOptions.Path, converter.GetFileName()));
            _logger.LogInformation("GetAll -- called, reading from " + fileName);

            using var package = new ExcelPackage(new FileInfo(fileName));
            if (!package.File.Exists) package.Workbook.Worksheets.Add("Formularsvar");
            var firstSheet = package.Workbook.Worksheets[0];
            firstSheet.Cells["A1"].Value = "Tidsstempel";
            firstSheet.Cells["B1"].Value = "Kontaktperson, navn";
            firstSheet.Cells["C1"].Value = "Kontaktperson, telefon";
            firstSheet.Cells["D1"].Value = "Kontaktperson, email";
            firstSheet.Cells["E1"].Value = "Skole / Institution, navn";
            firstSheet.Cells["F1"].Value = "Deltagergruppe";
            firstSheet.Cells["G1"].Value = "Deltagere, aldersinterval";
            firstSheet.Cells["H1"].Value = "Deltagere, antal";
            firstSheet.Cells["I1"].Value = "Ønsket arrangement";
            firstSheet.Cells["J1"].Value = "Ønsket mødested";
            firstSheet.Cells["K1"].Value = "Hvornår ønskes arrangement?";
            firstSheet.Cells["L1"].Value = "Ønsket dato for afholdelse";
            firstSheet.Cells["M1"].Value = "Bemærkninger";
            firstSheet.Cells["N1"].Value = "Institution eller skole";

            var firstEmptyRow = 0;
            for (var row = 2; row < firstSheet.Cells.Rows; row++)
            {
                var id = firstSheet.Cells[$"A{row}"].Text;
                if (string.IsNullOrEmpty(id))
                {
                    firstEmptyRow = row;
                    break;
                }
            }

            var rowValues = converter.GetValuesFromEntity(entity);
            for (var column = 0; column < rowValues.Count; column++)
            {
                var cellAddress = ((char) (65 + column)).ToString() + firstEmptyRow;
                firstSheet.Cells[cellAddress].Value = rowValues[column];
            }

            package.SaveAs(new FileInfo(fileName));

            return converter.GetEntity(rowValues) as T;
        }

        public Task UpdateAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        {
            throw new NotImplementedException();
        }
    }
}