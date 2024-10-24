namespace PetFamily.Domain.PetSpecies
{
    public record SpeciesId
    {
        private SpeciesId(Guid value) => Value = value;

        public Guid Value { get; }

        public static SpeciesId NewId() => new(Guid.NewGuid());
        public static SpeciesId Empty() => new(Guid.Empty);
        public static SpeciesId Create(Guid id) => new(id);
    }
}