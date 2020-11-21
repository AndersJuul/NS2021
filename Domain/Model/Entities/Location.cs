namespace Domain.Model.Entities
{
    public class Location:BaseEntity
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