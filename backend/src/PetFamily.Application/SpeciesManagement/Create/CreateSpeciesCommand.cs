using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.Create
{
    public record CreateSpeciesCommand(string Name) : ICommand;
}
