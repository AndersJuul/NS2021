namespace Domain.Model
{
    public class Counselor
    {
        public Counselor(string initials, string name, string phone, string email)
        {
            Initials = initials;
            Name = name;
            Phone = phone;
            Email = email;
        }
        public string Initials { get; }
        public string Name { get; }
        public string Phone { get; }
        public string Email { get; }
    }
}
