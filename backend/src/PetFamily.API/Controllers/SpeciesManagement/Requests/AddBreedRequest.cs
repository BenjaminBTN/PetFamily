using PetFamily.Application.SpeciesManagement.AddBreed;

namespace PetFamily.API.Controllers.SpeciesManagement.Requests
{
    public record AddBreedRequest(string Name)
    {
        public AddBreedCommand ToCommand(Guid id)
        {
            return new AddBreedCommand(id, Name);
        }
    }
}
