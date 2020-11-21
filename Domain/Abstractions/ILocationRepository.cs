using System.Collections.Generic;
using Domain.Model.Entities;

namespace Domain.Abstractions
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAll();
    }
}