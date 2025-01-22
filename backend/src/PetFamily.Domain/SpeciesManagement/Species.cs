using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Entities;
using PetFamily.Domain.SpeciesManagement.VO;
using System;
using System.Collections.Generic;

namespace PetFamily.Domain.SpeciesManagement
{
    public class Species : Shared.Entity<SpeciesId>
    {
        private readonly List<Breed> _breeds = [];

        public SpeciesName Name { get; private set; } = default!;
        public IReadOnlyList<Breed> Breeds => _breeds;


        private Species(SpeciesId id) : base(id) { }

        public Species(SpeciesId id, SpeciesName name) : base(id)
        {
            Name = name;
        }


        public Result<Guid, Error> Update(Species species, SpeciesName name)
        {
            Name = name;
            return species.Id.Value;
        }
    }
}
