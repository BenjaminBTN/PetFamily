using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.Volunteers.Entities;
using PetFamily.Domain.Volunteers.Enums;
using PetFamily.Domain.Volunteers.VO;
using System;
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
        public int Experience { get; private set; } = default;
        public PhoneNumber PhoneNumber { get; private set; } = default!;
        public VolunteerRequisitesList Requisites { get; private set; }
        public SocialNetworksList Networks { get; private set; }
        public DateTime CreationDate { get; } = DateTime.Now.ToUniversalTime();
        public IReadOnlyList<Pet> Pets => _pets;


        private Volunteer(VolunteerId id) : base(id) { }

        private Volunteer(
            VolunteerId id,
            FullName name,
            string description,
            Email email,
            int experience,
            PhoneNumber phoneNumber) 
            : base(id)
        {
            FullName = name;
            Description = description;
            Email = email;
            Experience = experience;
            PhoneNumber = phoneNumber;
            Requisites = new();
            Networks = new();
        }


        public int GetPetsSearchHomeCount() => _pets.Where(p => p.Status == AssistanceStatus.SearchHome).Count();
        public int GetPetsFoundHomeCount() => _pets.Where(p => p.Status == AssistanceStatus.FoundHome).Count();
        public int GetPetsNeedsHelpCount() => _pets.Where(p => p.Status == AssistanceStatus.NeedsHelp).Count();


        public static Result<Volunteer, Error> Create(
            VolunteerId id,
            FullName name,
            string description,
            Email email,
            int experience,
            PhoneNumber phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(description))
                return Errors.General.InvalidValue("Description");

            if(experience < 0)
                return Errors.General.InvalidValue("Experience");

            return new Volunteer(id, name, description, email, experience, phoneNumber);
        }


        public static Result<Guid, Error> Update(
            Volunteer volunteer,
            FullName? name,
            string description,
            Email? email,
            int experience,
            PhoneNumber? phoneNumber,
            VolunteerRequisitesList? requisites,
            SocialNetworksList? networks)
        {
            if(volunteer == null)
                return Errors.General.NullValue("Volunteer");

            if(name != null)
                volunteer.FullName = name;

            if(!string.IsNullOrWhiteSpace(description))
                volunteer.Description = description;

            if(email != null)
                volunteer.Email = email;

            if(experience >= 0)
                volunteer.Experience = experience;

            if(phoneNumber != null)
                volunteer.PhoneNumber = phoneNumber;

            if(requisites != null)
                volunteer.Requisites = requisites;

            if(networks != null)
                volunteer.Networks = networks;

            return volunteer.Id.Value;
        }
    }
}
