using System.Collections.Generic;

namespace CleanArchitecture.SharedKernel
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();

        public abstract void SetValues(string[] values);
    }
}