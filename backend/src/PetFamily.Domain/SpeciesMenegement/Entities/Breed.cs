using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesMenegement.VO;
using System;

namespace PetFamily.Domain.SpeciesMenegement.Entities
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


        public static Result<Guid, Error> Update(Breed breed, string name)
        {
            if(!string.IsNullOrWhiteSpace(name))
                breed.Name = name;

            return breed.Id.Value;
        }
    }
}
