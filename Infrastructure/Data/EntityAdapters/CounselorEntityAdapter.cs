using System;
using System.Collections.Generic;
using Domain.Model.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Data.EntityAdapters
{
    public class CounselorEntityAdapter : IEntityAdapter
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

        public List<string> GetValuesFromEntity(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}