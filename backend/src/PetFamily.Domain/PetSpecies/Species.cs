using CSharpFunctionalExtensions;

namespace PetFamily.Domain.PetSpecies
{
    public class Species : Shared.Entity<SpeciesId>
    {
        private readonly List<Breed> _breeds = [];

        private Species(SpeciesId id) : base(id) { }
        private Species(SpeciesId id, string name, List<Breed> breeds) : base(id)
        {
            Name = name;
            _breeds = breeds;
        }

        public string Name { get; private set; } = default!;
        public IReadOnlyList<Breed> Breeds => _breeds;

        public static Result<Species> Create(SpeciesId id, string name, List<Breed> breeds)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Result.Failure<Species>("Name can not be empty");

            return new Species(id, name, breeds);
        }
    }
}
