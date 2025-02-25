using PetFamily.Application.SpeciesManagement.Commands.Create;

namespace PetFamily.API.Controllers.SpeciesManagement.Requests;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand()
    {
        return new CreateSpeciesCommand(Name);
    }
}
