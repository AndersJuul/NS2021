using Domain.Model.ValueObjects;

namespace Domain.Model.Entities
{
    public class Counselor
    {
        public Counselor(string id, string name, PhoneNumber phone, string email)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
        }

        public string Id { get; }
        public string Name { get; }
        public PhoneNumber Phone { get; }
        public string Email { get; }
    }
}