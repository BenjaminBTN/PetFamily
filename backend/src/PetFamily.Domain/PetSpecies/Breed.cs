using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.PetSpecies
{
    public class Breed : Shared.Entity<BreedId>
    {
        public string Name { get; private set; } = default!;

        private Breed(BreedId id) : base(id) { }
        private Breed(BreedId id, string name) : base(id) => Name = name;

        public Result<Breed, Error> Create(BreedId id, string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Errors.General.InvalidValue("Name");

            return new Breed(id, name);
        }
    }
}
