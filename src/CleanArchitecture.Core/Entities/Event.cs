using System;
using CleanArchitecture.SharedKernel;
using CleanArchitecture.SharedKernel.Interfaces;

namespace CleanArchitecture.Core.Entities
{
    public class Event : BaseEntity, IAggregateRoot
    {
        public Event()
        {
            Description = "";
            Remember = "";
            ContactDaysBefore = "";
        }

        public Event(string id, string description, string remember, string contactDaysBefore)
        {
            Id = id;
            Description = description;
            Remember = remember;
            ContactDaysBefore = contactDaysBefore;
        }

        public string Description { get; private set; }
        public string Remember { get; private set; }
        public string ContactDaysBefore { get; private set; }

        public override void SetValues(string[] values)
        {
            if (values.Length != 4)
                throw new ArgumentException();
            Id = values[0];
            Description = values[1];
            Remember = values[2];
            ContactDaysBefore = values[3];
        }
    }
}