using System;

namespace Domain
{
    public class Counselor
    {
        public Counselor(string initials)
        {
            Initials = initials;
        }
        public string Initials { get; }
    }
}
