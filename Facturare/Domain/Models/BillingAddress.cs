using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturare.Domain.Models
{
    public record BillingAddress
    {
        public string Street { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public BillingAddress(string street, string city, string postalCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(postalCode) || string.IsNullOrWhiteSpace(country))
                throw new InvalidBillingAddressException("All fields of the billing address must be provided.");

            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
