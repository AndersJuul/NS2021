using Domain.Interfaces;

namespace Domain.Model.Entities
{
    public class Location : BaseEntity, IAggregateRoot
    {
        public Location(string id, string description)
        {
            Id = id;
            Description = description;
        }

        public string Description { get; }
    }
}