using System;

namespace PetFamily.Domain.PetSpecies
{
    public record BreedId
    {
        public Guid Value { get; }

        private BreedId(Guid value) => Value = value;  

        public static BreedId NewId() => new(Guid.NewGuid());
        public static BreedId Empty() => new(Guid.Empty);
        public static BreedId Create(Guid id) => new(id);
    }
}