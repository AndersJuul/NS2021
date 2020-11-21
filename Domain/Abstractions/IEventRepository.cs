using System.Collections.Generic;
using Domain.Model.Entities;

namespace Domain.Abstractions
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
    }
}