using System.Collections.Generic;

namespace Domain.Model.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber(string text)
        {
            Value = text;
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}