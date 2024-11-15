using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Collections.Generic;

namespace PetFamily.Domain.PetSpecies
{
    public class Species : Shared.Entity<SpeciesId>
    {
        private readonly List<Breed> _breeds = [];

        public string Name { get; private set; } = default!;
        public IReadOnlyList<Breed> Breeds => _breeds;

        private Species(SpeciesId id) : base(id) { }
        private Species(SpeciesId id, string name, List<Breed> breeds) : base(id)
        {
            Name = name;
            _breeds = breeds;
        }

        public static Result<Species, Error> Create(SpeciesId id, string name, List<Breed> breeds)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Errors.General.InvalidValue("Name");

            return new Species(id, name, breeds);
        }
    }
}