using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers
{
    public class Volunteer : Shared.Entity<VolunteerId>
    {
        private readonly List<Pet> _pets = [];

        private Volunteer(VolunteerId id) : base(id) { }
        private Volunteer(VolunteerId id, FullName name, string description) : base(id)
        {
            FullName = name;
            Description = description;
        }

        public FullName FullName { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public int Experience { get; private set; } = default!;
        public string PhoneNumber { get; private set; } = default!;
        public VolunteerDetails Details { get; private set; } = default!;
        public IReadOnlyList<Pet> Pets => _pets;

        public int GetPetsSearchHomeCount() => _pets.Where(p => p.Status == AssistanceStatus.SearchHome).Count();
        public int GetPetsFoundHomeCount() => _pets.Where(p => p.Status == AssistanceStatus.FoundHome).Count();
        public int GetPetsNeedsHelpCount() => _pets.Where(p => p.Status == AssistanceStatus.NeedsHelp).Count();

        public static Result<Volunteer> Create(VolunteerId id, FullName name, string description)
        {
            if(string.IsNullOrWhiteSpace(description))
                return Result.Failure<Volunteer>("Description can not be empty");

            return new Volunteer(id, name, description);
        }
    }
}
