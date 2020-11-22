using System;
using Domain.Interfaces;

namespace Domain.Model.Entities
{
    public class Location : BaseEntity, IAggregateRoot
    {
        public Location()
        {
            Description = "";
        }

        public Location(string id, string description)
        {
            Id = id;
            Description = description;
        }

        public string Description { get; private set; }

    }
}