using System;
using Infrastructure.Data.EntityAdapters;
using Infrastructure.Interfaces;
using OfficeOpenXml;

namespace Infrastructure.Data
{
    public class Stuff:IDisposable
    {
        public ExcelWorksheet firstSheet { get; set; }
        public IEntityAdapter converter { get; set; }
        public ExcelPackage package { get; set; }
        public string fileName { get; set; }

        public void Dispose()
        {
            package?.Dispose();
        }
    }
}