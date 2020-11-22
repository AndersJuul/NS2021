using System.Collections.Generic;

namespace CleanArchitecture.SharedKernel
{
    // This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();

        public abstract void SetValues(string[] values);
    }
}