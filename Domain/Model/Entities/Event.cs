namespace Domain.Model.Entities
{
    public class Event
    {
        public Event(string eventId, string description, string remember, string contactDaysBefore)
        {
            EventId = eventId;
            Description = description;
            Remember = remember;
            ContactDaysBefore = contactDaysBefore;
        }

        public string EventId { get; }
        public string Description { get; }
        public string Remember { get; }
        public string ContactDaysBefore { get; }
    }
}