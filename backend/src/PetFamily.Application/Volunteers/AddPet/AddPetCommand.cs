using PetFamily.Application.Providers.FileProvider.Dtos;

namespace PetFamily.Application.Volunteers.AddPet
{
    public record AddPetCommand(string Name, FileDto FileDto); // to modify later
}
