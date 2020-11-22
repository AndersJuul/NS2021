using System;
using System.Collections.Generic;
using Domain.Model.Entities;

namespace Infrastructure.Data.EntityAdapters
{
    public class EventEntityAdapter : IEntityAdapter
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

        public List<string> GetValuesFromEntity(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}