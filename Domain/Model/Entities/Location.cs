namespace Domain.Model.Entities
{
    public class Location
    {
        public string Id { get; }
        public string Description { get; }

        public Location(string id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}