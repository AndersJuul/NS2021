using Domain.Model.ValueObjects;

namespace Domain.Model.Entities
{
    public class Counselor
    {
        public Counselor(string initials, string name, PhoneNumber phone, string email)
        {
            Initials = initials;
            Name = name;
            Phone = phone;
            Email = email;
        }
        public string Initials { get; }
        public string Name { get; }
        public PhoneNumber Phone { get; }
        public string Email { get; }
    }
}
