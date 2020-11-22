using System;
using System.Collections.Generic;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Infrastructure.Data.EntityAdapters
{
    public interface IEntityAdapter
    {
        bool CanHandle(Type type);
        string GetFileName();
        BaseEntity GetEntity(List<string> rowValues);
    }
}