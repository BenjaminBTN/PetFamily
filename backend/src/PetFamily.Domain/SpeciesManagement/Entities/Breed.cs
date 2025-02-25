using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.VO;
using System;

namespace PetFamily.Domain.SpeciesManagement.Entities;

public class Breed : Shared.Entity<BreedId>
{
    public BreedName Name { get; private set; } = default!;


    private Breed(BreedId id) : base(id) { }

    public Breed(BreedId id, BreedName name) : base(id) => Name = name;


    public Result<Guid, Error> Update(Breed breed, BreedName name)
    {
        Name = name;
        return breed.Id.Value;
    }
}
