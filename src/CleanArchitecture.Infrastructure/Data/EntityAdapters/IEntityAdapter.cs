using System;
using System.Collections.Generic;
using CleanArchitecture.SharedKernel;
using CleanArchitecture.SharedKernel.Interfaces;

namespace CleanArchitecture.Infrastructure.Data.EntityAdapters
{
    public interface IEntityAdapter
    {
        bool CanHandle(Type type);
        string GetFileName();
        BaseEntity GetEntity(List<string> rowValues);
        List<string> GetValuesFromEntity(BaseEntity entity);
    }
}