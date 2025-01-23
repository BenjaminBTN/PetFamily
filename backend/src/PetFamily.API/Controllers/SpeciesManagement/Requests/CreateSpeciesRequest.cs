using PetFamily.Application.SpeciesManagement.Create;

namespace PetFamily.API.Controllers.SpeciesManagement.Requests
{
    public record CreateSpeciesRequest(string Name)
    {
        public CreateSpeciesCommand ToCommand()
        {
            return new CreateSpeciesCommand(Name);
        }
    }
}
