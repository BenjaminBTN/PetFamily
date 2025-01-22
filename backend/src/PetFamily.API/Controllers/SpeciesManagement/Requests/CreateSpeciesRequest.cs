using PetFamily.Application.SpeciesManagement.Create;

namespace PetFamily.API.Controllers.SpeciesManagement.Requests
{
    public record CreateSpeciesRequest()
    {
        public CreateSpeciesCommand ToCommand()
        {
            return new CreateSpeciesCommand();
        }
    }
}
