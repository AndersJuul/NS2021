using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;
using CleanArchitecture.SharedKernel.Interfaces;
using Domain.Model.ValueObjects;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace CleanArchitecture.Infrastructure.Data
{
    public class ExcelRepository:IRepository
    {
        private readonly ILogger<ExcelRepository> _logger;
        private readonly IEnumerable<IConverter> _converters;

        public ExcelRepository(ILogger<ExcelRepository> logger, IEnumerable<IConverter> converters)
        {
            _logger = logger;
            _converters = converters;
        }
        public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity, IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity, IAggregateRoot
        {
            var type = typeof(T);
            var converter = _converters.Single(x => x.CanHandle(type));
            var fn = converter.GetFileName();
            var fileName = Path.GetFullPath(Path.Combine(@"C:\Projects\_NS2020\Tests\Testdata", fn));
            _logger.LogInformation("GetAll -- called, reading from " + fileName);

            using var package = new ExcelPackage(new FileInfo(fileName));

            var firstSheet = package.Workbook.Worksheets[0];

            var result= new List<T>();

            for (var row = 2; row < firstSheet.Cells.Rows; row++)
            {
                var id = firstSheet.Cells[$"A{row}"].Text;
                if (string.IsNullOrEmpty(id))
                    break;

                var rowValues=new List<string>(){id};
                for (var column = 2; column < firstSheet.Cells.Columns; column++)
                {
                    var cellAddress = ((char) (64 + column)).ToString() + row;
                    var cellValue = firstSheet.Cells[cellAddress].Text;
                    if(string.IsNullOrEmpty( cellValue))
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

        public Task<T> AddAsync<T>(T entity) where T : BaseEntity, IAggregateRoot
        {
            throw new NotImplementedException();
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

    public interface IConverter
    {
        bool CanHandle(Type type);
        string GetFileName();
        BaseEntity GetEntity(List<string> rowValues);
    }

    public class LocationConverter : IConverter
    {
        public bool CanHandle(Type type)
        {
            return type == typeof(Location);
        }

        public string GetFileName()
        {
            return "Locations.xlsx";
        }

        public BaseEntity GetEntity(List<string> rowValues)
        {
            return new Location(rowValues[0], rowValues[1]);
        }
    }

    public class EventConverter : IConverter
    {
        public bool CanHandle(Type type)
        {
            return  type == typeof(Event);
        }

        public string GetFileName()
        {
            return "Events.xlsx";
        }

        public BaseEntity GetEntity(List<string> rowValues)
        {
            return new Event(rowValues[0], rowValues[1], rowValues[2], rowValues[3]);
        }
    }

    public class CounselorConverter : IConverter
    {
        public bool CanHandle(Type type)
        {
            return type == typeof(Counselor);
        }

        public string GetFileName()
        {
            return "Counselors.xlsx";
        }

        public BaseEntity GetEntity(List<string> rowValues)
        {
            return new Counselor(rowValues[0], rowValues[1],new PhoneNumber(  rowValues[2]), rowValues[3]);
        }
    }
}
