using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.Volunteers.Entities;
using PetFamily.Domain.Volunteers.Enums;
using PetFamily.Domain.Volunteers.VO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Volunteers
{
    public class Volunteer : Shared.Entity<VolunteerId>, IDeletable
    {
        private readonly List<Pet> _pets = [];
        private bool _isDeleted = false;


        public FullName FullName { get; private set; } = default!;
        public Description Description { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public double Experience { get; private set; } = default;
        public PhoneNumber PhoneNumber { get; private set; } = default!;
        public VolunteerRequisiteList Requisites { get; private set; }
        public SocialNetworkList Networks { get; private set; }
        public DateTime CreationDate { get; } = DateTime.Now.ToLocalTime();
        public IReadOnlyList<Pet> Pets => _pets;


        private Volunteer(VolunteerId id) : base(id) { }

        private Volunteer(
            VolunteerId id,
            FullName name,
            Description description,
            Email email,
            double experience,
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
            Description description,
            Email email,
            double experience,
            PhoneNumber phoneNumber)
        {
            if(experience < 0)
                return Errors.General.InvalidValue("Experience");

            return new Volunteer(id, name, description, email, experience, phoneNumber);
        }


        public void Delete()
        {
            if(_isDeleted ==  false)
                _isDeleted = true;

            foreach(var pet in _pets)
                pet.Delete();
        }


        public void Restore()
        {
            if(_isDeleted == true)
                _isDeleted = false;

            foreach(var pet in _pets)
                pet.Restore();
        }


        public void UpdateMainInfo(
            FullName name,
            Description description,
            Email email,
            double experience,
            PhoneNumber phoneNumber)
        {
            FullName = name;
            Description = description;
            Email = email;
            Experience = experience;
            PhoneNumber = phoneNumber;
        }


        public void UpdateRequisites(VolunteerRequisiteList requisites)
        {
            Requisites = requisites;
        }


        public void UpdateSocialNetworks(SocialNetworkList networks)
        {
            Networks = networks;
        }
    }
}
