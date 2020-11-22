using System;
using System.Collections.Generic;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Infrastructure.Data.EntityAdapters
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
    }
}