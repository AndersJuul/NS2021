using System;
using Domain.Interfaces;

namespace Domain.Model.Entities
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

    }
}