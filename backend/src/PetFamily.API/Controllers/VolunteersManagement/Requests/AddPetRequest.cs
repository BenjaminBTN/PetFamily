using PetFamily.Application.VolunteersManagement.AddPet;
using PetFamily.Application.VolunteersManagement.Dtos;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record AddPetRequest(
        string Name,
        string Description,
        string Species,
        string Breed,
        string Color,
        string HealthInfo,
        string Country,
        string Region,
        string City,
        string Street,
        int HouseNumber,
        string PostalCode,
        double Weight,
        double Height,
        string PhoneNumber,
        bool IsCastrated,
        bool IsVaccinated,
        string? BirthDate,
        int Status)
    {
        public AddPetCommand ToCommand(Guid id)
        {
            var typeInfo = new PetTypeDto(Species, Breed);
            var address = new AddressDto(Country, Region, City, Street, HouseNumber, PostalCode);

            return new AddPetCommand(
                id, Name, Description, typeInfo, Color, HealthInfo,
                address, Weight, Height, PhoneNumber, IsCastrated, 
                IsVaccinated, BirthDate, Status);
        }
    }
}
