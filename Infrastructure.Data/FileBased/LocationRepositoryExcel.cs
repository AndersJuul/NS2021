using System.Collections.Generic;
using Domain.Abstractions;
using Domain.Model.Entities;

namespace Infrastructure.Data.FileBased
{
    public class LocationRepositoryExcel : ILocationRepository
    {
        public IEnumerable<Location> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}