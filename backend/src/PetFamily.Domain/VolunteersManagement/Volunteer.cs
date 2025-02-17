using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement.Entities;
using PetFamily.Domain.VolunteersManagement.Enums;
using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.VolunteersManagement
{
    public class Volunteer : Shared.Entity<VolunteerId>, IDeletable
    {
        private readonly List<Pet> _pets = new();
        private List<VolunteerRequisite> _requisites = [];
        private List<SocialNetwork> _networks = [];
        private bool _isDeleted = false;

        public FullName FullName { get; private set; } = default!;
        public Description Description { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public double Experience { get; private set; } = default;
        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public DateTime CreationDate { get; } = DateTime.Now.ToLocalTime();

        public IReadOnlyList<VolunteerRequisite> Requisites => _requisites;
        public IReadOnlyList<SocialNetwork> Networks => _networks;
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
            if(experience < 0 || experience > 99)
                return Errors.General.InvalidValue("Experience");

            return new Volunteer(id, name, description, email, experience, phoneNumber);
        }


        public void Delete()
        {
            if(_isDeleted == false)
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


        public UnitResult<Error> AddPet(Pet pet)
        {
            if(_isDeleted == true)
                return Error.Failure("entity.is.deleted", "Can not add a pet to a deleted volunteer");

            var ordinalNumberResult = OrdinalNumber.Create(_pets.Count + 1);
            if(ordinalNumberResult.IsFailure)
                return ordinalNumberResult.Error;

            pet.SetOrdinalNumber(ordinalNumberResult.Value);

            _pets.Add(pet);

            return UnitResult.Success<Error>();
        }


        public Result<Pet, Error> GetPetById(PetId id)
        {
            var result = _pets.FirstOrDefault(p => p.Id == id);
            if(result == null)
                return Errors.General.NotFound(id.Value);

            return result;
        }


        public UnitResult<Error> MovePet(Pet pet, OrdinalNumber newOrdinalNumber)
        {
            var currentOrdinalNumber = pet.OrdinalNumber;

            if(currentOrdinalNumber == newOrdinalNumber || _pets.Count == 1)
                return UnitResult.Success<Error>();

            if(newOrdinalNumber.Value > _pets.Count)
            {
                var lastPositionResult = OrdinalNumber.Create(_pets.Count);
                if(lastPositionResult.IsFailure)
                    return lastPositionResult.Error;

                newOrdinalNumber = lastPositionResult.Value;
            }            

            var result = ChangeOtherPetPositions(currentOrdinalNumber, newOrdinalNumber);
            if(result.IsFailure)
                return result.Error;

            pet.SetOrdinalNumber(newOrdinalNumber);

            return UnitResult.Success<Error>();
        }


        private UnitResult<Error> ChangeOtherPetPositions(
            OrdinalNumber currentOrdinalNumber,
            OrdinalNumber newOrdinalNumber)
        {
            if(newOrdinalNumber.Value > currentOrdinalNumber.Value)
            {
                var petsToMove = _pets.Where(p => p.OrdinalNumber.Value <= newOrdinalNumber.Value &&
                                                  p.OrdinalNumber.Value > currentOrdinalNumber.Value);

                foreach(var petToMove in petsToMove)
                {
                    var result = petToMove.MoveBack();
                    if(result.IsFailure)
                        return result.Error;
                }
            }

            else if(newOrdinalNumber.Value < currentOrdinalNumber.Value)
            {
                var petsToMove = _pets.Where(p => p.OrdinalNumber.Value >= newOrdinalNumber.Value &&
                                                  p.OrdinalNumber.Value < currentOrdinalNumber.Value);

                foreach(var petToMove in petsToMove)
                {
                    var result = petToMove.MoveForward();
                    if(result.IsFailure)
                        return result.Error;
                }
            }

            return UnitResult.Success<Error>();
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


        public void UpdateRequisites(IEnumerable<VolunteerRequisite> requisites)
        {
            _requisites = [.. requisites];
        }


        public void UpdateSocialNetworks(IEnumerable<SocialNetwork> networks)
        {
            _networks = [..networks];
        }
    }
}