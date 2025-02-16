namespace PetFamily.Application.Dtos;

public class AddressDto(string country, string region, string city, string street, int houseNumber, string postalCode)
{
    public string Country { get; init; } = country;
    public string Region { get; init; } = region;
    public string City { get; init; } = city;
    public string Street { get; init; } = street;
    public int HouseNumber { get; init; } = houseNumber;
    public string PostalCode { get; init; } = postalCode;
}
