using System.Collections.Generic;
using Domain.Events;

namespace Domain.Model.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();

    }
}