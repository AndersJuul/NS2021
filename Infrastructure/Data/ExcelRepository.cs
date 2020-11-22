using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Domain.Interfaces;
using Domain.Model.Entities;
using Infrastructure.Data.EntityAdapters;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace Infrastructure.Data
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
            using var stuff = GetStuff<T>();

            var result = new List<T>();

            for (var row = 2; row <stuff.firstSheet.Cells.Rows; row++)
            {
                var id =stuff. firstSheet.Cells[$"A{row}"].Text;
                if (string.IsNullOrEmpty(id))
                    break;

                var rowValues = new List<string> {id};
                for (var column = 2; column <stuff. firstSheet.Cells.Columns; column++)
                {
                    var cellAddress = ((char) (64 + column)).ToString() + row;
                    var cellValue = stuff.firstSheet.Cells[cellAddress].Text;
                    if (string.IsNullOrEmpty(cellValue))
                        break;
                    rowValues.Add(cellValue);
                }

                result.Add(stuff.converter.GetEntity(rowValues) as T);
            }

            return result;
        }

        private Stuff GetStuff<T>()
        {
            var type = typeof(T);
            var converter = _converters.SingleOrDefault(x => x.CanHandle(type));
            if (converter == null)
                throw new ArgumentException("No converter found for " + type.FullName);

            var fileName = GetFullPath(converter);
            _logger.LogInformation("GetAll -- called, reading from " + fileName);

            var package = new ExcelPackage(new FileInfo(fileName));
            if (!package.File.Exists)
            {
                package.Workbook.Worksheets.Add("Sheet0");
            }
                
            var firstSheet = package.Workbook.Worksheets[0];

            return new Stuff
            {
                package = package,
                firstSheet = firstSheet,
                converter = converter,
                fileName = fileName
            };
        }

        public Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseEntity, IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        {
            using var stuff = GetStuff<T>();

           stuff. firstSheet.Cells["A1"].Value = "Tidsstempel";
           stuff.firstSheet.Cells["B1"].Value = "Kontaktperson, navn";
           stuff.firstSheet.Cells["C1"].Value = "Kontaktperson, telefon";
           stuff.firstSheet.Cells["D1"].Value = "Kontaktperson, email";
           stuff.firstSheet.Cells["E1"].Value = "Skole / Institution, navn";
           stuff.firstSheet.Cells["F1"].Value = "Deltagergruppe";
           stuff.firstSheet.Cells["G1"].Value = "Deltagere, aldersinterval";
           stuff.firstSheet.Cells["H1"].Value = "Deltagere, antal";
           stuff.firstSheet.Cells["I1"].Value = "Ønsket arrangement";
           stuff.firstSheet.Cells["J1"].Value = "Ønsket mødested";
           stuff.firstSheet.Cells["K1"].Value = "Hvornår ønskes arrangement?";
           stuff.firstSheet.Cells["L1"].Value = "Ønsket dato for afholdelse";
           stuff.firstSheet.Cells["M1"].Value = "Bemærkninger";
           stuff.firstSheet.Cells["N1"].Value = "Institution eller skole";

            var firstEmptyRow = 0;
            for (var row = 2; row < stuff.firstSheet.Cells.Rows; row++)
            {
                var id = stuff.firstSheet.Cells[$"A{row}"].Text;
                if (string.IsNullOrEmpty(id))
                {
                    firstEmptyRow = row;
                    break;
                }
            }

            var rowValues = stuff.converter.GetValuesFromEntity(entity);
            for (var column = 0; column < rowValues.Count; column++)
            {
                var cellAddress = ((char) (65 + column)).ToString() + firstEmptyRow;
                stuff.firstSheet.Cells[cellAddress].Value = rowValues[column];
            }

            stuff.package.SaveAs(new FileInfo(stuff.fileName));

            return stuff.converter.GetEntity(rowValues) as T;
        }

        private string GetFullPath(IEntityAdapter converter) 
        {
            Directory.CreateDirectory(_fileLocationOptions.WorkRoot);
            var fileName = converter.GetFileName();

            var destFileName = Path.Combine(_fileLocationOptions.WorkRoot, fileName);
            var sourceFileName = Path.Combine("DefaultInput", fileName);
            if (!File.Exists(destFileName) && File.Exists(sourceFileName))
            {
                File.Copy(sourceFileName, destFileName);
            }

            return Path.GetFullPath(destFileName);
        }

        public Task UpdateAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public async Task AddOrUpdateAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        {
            using var stuff = GetStuff<T>();

            for (var row = 2; row < stuff.firstSheet.Cells.Rows; row++)
            {
                var id = stuff.firstSheet.Cells[$"A{row}"].Text;
                if (string.IsNullOrEmpty(id))
                {
                    //End of file reached. We add.
                    await AddAsync(entity);
                    break;
                }

                if (id == entity.Id)
                {
                    // Entity found. We update.
                    
                    var rowValues = stuff.converter.GetValuesFromEntity(entity);
                    for (var column = 0; column < rowValues.Count; column++)
                    {
                        var cellAddress = ((char)(65 + column)).ToString() + row;
                        stuff.firstSheet.Cells[cellAddress].Value = rowValues[column];
                    }

                    stuff.package.SaveAs(new FileInfo(stuff.fileName));
                    break;
                }
            }
        }
    }
}