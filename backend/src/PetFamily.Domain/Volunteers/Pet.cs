using CSharpFunctionalExtensions;
using PetFamily.Domain.PetSpecies;

namespace PetFamily.Domain.Volunteers
{
    public class Pet : Shared.Entity<PetId>
    {
        private readonly List<PetPhoto> _petPhotos = [];


        private Pet(PetId id) : base(id) { }

        private Pet(PetId id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }


        public string Name { get; private set; } = default!;

        public Species Species { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public Breed Breed { get; private set; } = default!;

        public string Color { get; private set; } = default!;

        public string HealthInfo { get; private set; } = default!;

        public string Adress { get; private set; } = default!;

        public double Weight { get; private set; } = default!;

        public double Height { get; private set; } = default!;

        public string PhoneNumber { get; private set; } = default!;

        public bool IsCastrated { get; private set; } = default!;

        public bool IsVaccinated { get; private set; } = default!;

        public DateTime BirthDate { get; private set; } = default!;

        public AssistanceStatus Status { get; private set; } = default!;

        public PetRequisites Requisites { get; private set; } = default!;

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        //public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;


        public static Result<Pet> Create(PetId id, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Pet>("Name can not be empty");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Pet>("Description can not be empty");

            var pet = new Pet(id, name, description);

            return Result.Success(pet);
        }
    }
}
