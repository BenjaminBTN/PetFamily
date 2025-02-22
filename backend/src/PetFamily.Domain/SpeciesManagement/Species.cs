using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Entities;
using PetFamily.Domain.SpeciesManagement.VO;
using System.Collections.Generic;
using System.Linq;

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


        public void AddBreed(Breed breed)
        {
            _breeds.Add(breed);
        }


        public void DeleteBreed(Breed breed)
        {
            _breeds.Remove(breed);
        }


        public Result<Breed, Error> GetBreedById(BreedId id)
        {
            var result = _breeds.FirstOrDefault(b => b.Id == id);
            if (result == null)
                return Errors.General.NotFound(id.Value);

            return result;
        }


        public Result<Breed, Error> GetBreedByName(BreedName name)
        {
            var result = _breeds.FirstOrDefault(b => b.Name == name);
            if(result == null)
                return Errors.General.NotFound("Breed Name");

            return result;
        }


        public void Update(SpeciesName name)
        {
            Name = name;
        }
    }
}
