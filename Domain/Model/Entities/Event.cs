namespace Domain.Model.Entities
{
    public class Event
    {
        public Event(string id, string description, string remember, string contactDaysBefore)
        {
            Id = id;
            Description = description;
            Remember = remember;
            ContactDaysBefore = contactDaysBefore;
        }

        public string Id { get; }
        public string Description { get; }
        public string Remember { get; }
        public string ContactDaysBefore { get; }
    }
}