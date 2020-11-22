using System;
using System.Collections.Generic;
using Domain.Model.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Data.EntityAdapters
{
    public class ResultEntityAdapter : IEntityAdapter
    {
        public bool CanHandle(Type type)
        {
            return type == typeof(Result);
        }

        public string GetFileName()
        {
            return "Results.xlsx";
        }

        public BaseEntity GetEntity(List<string> rowValues)
        {
            return new Result(rowValues[0]);
        }

        public List<string> GetValuesFromEntity(BaseEntity entity)
        {
            var result = entity as Result;
            return new List<string>
            {
                result.Id
            };
        }
    }
}