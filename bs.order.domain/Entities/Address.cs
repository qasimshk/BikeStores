using bs.component.sharedkernal.Abstractions;
using System.Collections.Generic;

namespace bs.order.domain.Entities
{
    public class Address : ValueObject
    {
        protected Address() { }

        public Address(string street, string city, string country, string postCode)
        {
            Street = street;
            City = city;
            Country = country;
            PostCode = postCode;
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string PostCode { get; private set; }
        
        public override string ToString()
        {
            return $"{Street} {PostCode} {City} {Country}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return Country;
            yield return PostCode;
        }
    }
}
