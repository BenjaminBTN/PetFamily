using CSharpFunctionalExtensions;

namespace PetFamily.Domain.PetSpecies
{
    public class Breed : Shared.Entity<BreedId>
    {
        private Breed(BreedId id) : base(id) { }
        private Breed(BreedId id, string name) : base(id)
        {
            
        }

        public string Name { get; private set; } = default!;

        public Result<Breed> Create(BreedId id, string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Result.Failure<Breed>("Name can not be empty");

            return new Breed(id, name);
        }
    }
}
