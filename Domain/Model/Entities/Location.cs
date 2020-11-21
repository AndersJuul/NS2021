namespace Domain.Model.Entities
{
    public class Location
    {
        public Location(string id, string description)
        {
            Id = id;
            Description = description;
        }

        public string Id { get; }
        public string Description { get; }
    }
}