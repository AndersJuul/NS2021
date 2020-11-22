using System;
using CleanArchitecture.SharedKernel;
using CleanArchitecture.SharedKernel.Interfaces;
using Domain.Model.ValueObjects;

namespace CleanArchitecture.Core.Entities
{
    public class Counselor:BaseEntity, IAggregateRoot
    {
        public Counselor()
        {
            Name = "";
            Phone = new PhoneNumber("");
            Email = "";
        }
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

        public override void SetValues(string[] values)
        {
            if (values.Length != 4)
                throw new ArgumentException();
            Id = values[0];
            Name = values[1];
            Phone =new PhoneNumber( values[2]);
            Email = values[3];
        }
    }
}