using PetFamily.Application.Providers.Dtos;

namespace PetFamily.Application.Pets.Create
{
    public record CreatePetCommand(string Name, FileDto FileDto); // to modify later
}
