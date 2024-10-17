using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers
{
    public class Volunteer : Shared.Entity<VolunteerId>
    {
        private readonly List<SocialNetwork> _networks = [];
        private readonly List<VolunteerRequisites> _requisites = [];
        private readonly List<Pet> _pets = [];

        private Volunteer(VolunteerId id) : base(id) { }

        private Volunteer(VolunteerId id, string name, string description) : base(id)
        {
            FullName = name;
            Description = description;
        }

        public string FullName { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public int Experience { get; private set; } = default!;

        public string PhoneNumber { get; private set; } = default!;



        public int GetPetsSearchHomeCount() => _pets.Where(p => p.Status == AssistanceStatus.SearchHome).Count();

        public int GetPetsFoundHomeCount() => _pets.Where(p => p.Status == AssistanceStatus.FoundHome).Count();

        public int GetPetsNeedsHelpCount() => _pets.Where(p => p.Status == AssistanceStatus.NeedsHelp).Count();

        

        public IReadOnlyList<SocialNetwork> Networks => _networks;

        public IReadOnlyList<VolunteerRequisites> Requisites => _requisites;

        public IReadOnlyList<Pet> Pets => _pets;


        public static Result<Volunteer> Create(VolunteerId id, string name, string surname)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Result.Failure<Volunteer>("Name can not be empty");

            if(string.IsNullOrWhiteSpace(surname))
                return Result.Failure<Volunteer>("Surname can not be empty");

            var volunteer = new Volunteer(id, name, surname);

            return Result.Success(volunteer);
        }
    }
}
