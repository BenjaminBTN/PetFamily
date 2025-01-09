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

        public string Name { get; private set; } = default!;
        public IReadOnlyList<Breed> Breeds => _breeds;


        private Species(SpeciesId id) : base(id) { }

        private Species(SpeciesId id, string name) : base(id)
        {
            Name = name;
        }


        public static Result<Species, Error> Create(SpeciesId id, string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Errors.General.InvalidValue("Name");

            if(name.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.OverMaxLength("Name");

            return new Species(id, name);
        }


        public static Result<Guid, Error> Update(Species species, string name)
        {
            if(!string.IsNullOrWhiteSpace(name))
                species.Name = name;

            if(name.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.OverMaxLength("Name");

            return species.Id.Value;
        }
    }
}