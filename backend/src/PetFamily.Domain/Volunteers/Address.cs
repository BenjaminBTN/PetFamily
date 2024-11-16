using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers
{
    public record Address
    {
        public const int POSTAL_CODE_LENGTH = 6;

        public string Country { get; }
        public string Region { get; }
        public string City { get; }
        public string Street { get; }
        public int HouseNumber { get; }
        public string PostalCode { get; }

        private Address(string country, string region, string city, string street, int houseNumber, string postalCode)
        {
            Country = country;
            Region = region;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
        }

        public static Result<Address, Error> Create(string country, string region, string city, string street, int houseNumber, string postalCode)
        {
            if(string.IsNullOrWhiteSpace(country))
                return Errors.General.InvalidValue("Country");

            if(string.IsNullOrWhiteSpace(region))
                return Errors.General.InvalidValue("Region");

            if(string.IsNullOrWhiteSpace(city))
                return Errors.General.InvalidValue("City");

            if(string.IsNullOrWhiteSpace(street))
                return Errors.General.InvalidValue("Street");

            if(houseNumber < 1)
                return Errors.General.InvalidValue("House number");

            if(string.IsNullOrWhiteSpace(postalCode) || postalCode.Length < POSTAL_CODE_LENGTH)
                return Errors.General.InvalidValue("Postal code");

            return new Address(country, region, city, street, houseNumber, postalCode);
        }
    }
}