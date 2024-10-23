using CSharpFunctionalExtensions;
using PetFamily.Domain.PetSpecies;

namespace PetFamily.Domain.Volunteers
{
    public class Pet : Shared.Entity<PetId>
    {
        private Pet(PetId id) : base(id) { }

        private Pet(PetId id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }


        public string Name { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        //public Guid SpeciesId { get; private set; } = default!;

        //public Guid BreedId { get; private set; } = default!;

        public string Color { get; private set; } = default!;

        public string HealthInfo { get; private set; } = default!;

        public Address Address { get; private set; } = default!;

        public double Weight { get; private set; } = default!;

        public double Height { get; private set; } = default!;

        public string PhoneNumber { get; private set; } = default!;

        public bool IsCastrated { get; private set; } = default!;

        public bool IsVaccinated { get; private set; } = default!;

        public DateTime? BirthDate { get; private set; } = default!;

        public AssistanceStatus Status { get; private set; } = default!;

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        public PetDetails Details { get; private set; } = default!;


        public static Result<Pet> Create(PetId id, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Pet>("Name can not be empty");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Pet>("Description can not be empty");

            return new Pet(id, name, description); 
        }
    }
}
