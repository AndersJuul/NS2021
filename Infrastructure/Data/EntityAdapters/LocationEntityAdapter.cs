﻿using System;
using System.Collections.Generic;
using Domain.Model.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Data.EntityAdapters
{
    public class LocationEntityAdapter : IEntityAdapter
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

        public List<string> GetValuesFromEntity(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}