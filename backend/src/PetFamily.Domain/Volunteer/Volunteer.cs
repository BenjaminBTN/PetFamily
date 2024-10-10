using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet;

namespace PetFamily.Domain.Volunteer
{
    public class Volunteer
    {
        private readonly List<SocialNetwork> _networks = [];
        private readonly List<Requisites> _requisites = [];
        private readonly List<Pet.Pet> _pets = [];

        private Volunteer() { }

        private Volunteer(string name, string surname)
        {
            Name = name;
            SurName = surname;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;

        public string MiddleName { get; private set; } = default!;

        public string SurName { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public int Experience { get; private set; } = default!;

        public int GetPetsSearchHomeCount() => _pets.Where(p => p.Status == AssistanceStatus.SearchHome).Count();

        public int GetPetsFoundHomeCount() => _pets.Where(p => p.Status == AssistanceStatus.FoundHome).Count();

        public int GetPetsNeedsHelpCount() => _pets.Where(p => p.Status == AssistanceStatus.NeedsHelp).Count();

        public string PhoneNumber { get; private set; } = default!;

        public IReadOnlyList<SocialNetwork> Networks => _networks;

        public IReadOnlyList<Requisites> Requisites => _requisites;

        public IReadOnlyList<Pet.Pet> Pets => _pets;


        public static Result<Volunteer> Create(string name, string surname)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Volunteer>("Name can not be empty");
            }

            if(String.IsNullOrWhiteSpace(surname))
            {
                return Result.Failure<Volunteer>("Surname can not be empty");
            }

            var volunteer = new Volunteer(name, surname);

            return Result.Success(volunteer);
        }
    }
}
