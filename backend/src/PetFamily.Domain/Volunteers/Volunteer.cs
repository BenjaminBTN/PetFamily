using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Volunteers
{
    public class Volunteer : Shared.Entity<VolunteerId>
    {
        private readonly List<Pet> _pets = [];

        public FullName FullName { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public int Experience { get; private set; } = default!;
        public PhoneNumber PhoneNumber { get; private set; } = default!;
        public VolunteerDetails? Details { get; private set; } = default!;
        public IReadOnlyList<Pet> Pets => _pets;

        private Volunteer(VolunteerId id) : base(id) { }
        private Volunteer(VolunteerId id, FullName name, string description, Email email,
            int experience, PhoneNumber phoneNumber) : base(id)
        {
            FullName = name;
            Description = description;
            Email = email;
            Experience = experience;
            PhoneNumber = phoneNumber;
        }

        public int GetPetsSearchHomeCount() => _pets.Where(p => p.Status == AssistanceStatus.SearchHome).Count();
        public int GetPetsFoundHomeCount() => _pets.Where(p => p.Status == AssistanceStatus.FoundHome).Count();
        public int GetPetsNeedsHelpCount() => _pets.Where(p => p.Status == AssistanceStatus.NeedsHelp).Count();

        public static Result<Volunteer, Error> Create(VolunteerId id, FullName name, string description, Email email, 
            int experience, PhoneNumber phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(description))
                return Errors.General.InvalidValue("Description");

            if(experience < 0)
                return Errors.General.InvalidValue("Experience");

            return new Volunteer(id, name, description, email, experience, phoneNumber);
        }
    }
}