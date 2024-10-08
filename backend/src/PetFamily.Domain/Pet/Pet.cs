using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Pet
{
    public class Pet
    {
        private Pet()
        {
            
        }

        private Pet(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;

        public Spices Spices { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public Breed Breed { get; private set; } = default!;

        public string Color { get; private set; } = default!;

        public string HealthInfo { get; private set; } = default!;

        public string Adress {  get; private set; } = default!;

        public double Weight { get; private set; } = default!;

        public double Height { get; private set; } = default!;

        public string PhonNumber { get; private set; } = default!;

        public bool Castrated { get; private set; } = default!;

        public DateTime BirthDate { get; private set; } = default!;

        public bool Vaccinated { get; private set; } = default!;

        public AssistanceStatus Status { get; private set; } = default!;

        public Requisites Requisites { get; private set; } = default!;

        public DateTime CreationDate { get; private set; } = DateTime.Now;


        public static Result<Pet> Create(string name, string description)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Pet>("Name can not be empty");
            }

            if(String.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<Pet>("Description can not be empty");
            }

            var pet = new Pet(name, description);

            return Result.Success(pet);
        }
    }

    
}
