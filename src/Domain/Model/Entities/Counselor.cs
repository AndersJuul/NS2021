using System;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Model.Entities
{
    public class Counselor:BaseEntity, IAggregateRoot
    {
        public Counselor(string id, string name, PhoneNumber phone, string email)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
        }

        public string Name { get; private set; }
        public PhoneNumber Phone { get; private set; }
        public string Email { get; private set; }
    }
}