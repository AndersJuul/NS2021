using Domain.Interfaces;

namespace Domain.Model.Entities
{
    public class Event : BaseEntity, IAggregateRoot
    {
        public Event(string id, string description, string remember, string contactDaysBefore)
        {
            Id = id;
            Description = description;
            Remember = remember;
            ContactDaysBefore = contactDaysBefore;
        }

        public string Description { get; }
        public string Remember { get; }
        public string ContactDaysBefore { get; }
    }
}