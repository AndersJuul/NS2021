using System;
using System.Collections.Generic;
using Domain.Model.Entities;

namespace Infrastructure.Interfaces
{
    public interface IEntityAdapter
    {
        bool CanHandle(Type type);
        string GetFileName();
        BaseEntity GetEntity(List<string> rowValues);
        List<string> GetValuesFromEntity(BaseEntity entity);
    }
}