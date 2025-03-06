using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteersManagement.Commands.UpdatePet;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests;

public record class UpdatePetRequest(
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
    List<RequisiteForHelpDto> RequisitesForHelp)
{
    public UpdatePetCommand ToCommand(Guid id, Guid petId)
    {
        var typeInfo = new PetTypeDto(Species, Breed);
        var address = new AddressDto(Country, Region, City, Street, HouseNumber, PostalCode);

        return new UpdatePetCommand(
            id,
            petId,
            Name,
            Description,
            typeInfo,
            Color,
            HealthInfo,
            address,
            Weight,
            Height,
            PhoneNumber,
            IsCastrated,
            IsVaccinated,
            BirthDate,
            RequisitesForHelp);
    }
}
