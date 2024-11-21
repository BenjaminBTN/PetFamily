using CSharpFunctionalExtensions;
using PetFamily.Domain.PetManagement.Enums;
using PetFamily.Domain.PetManagement.VO;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using System;

namespace PetFamily.Domain.PetManagement.Entities
{
    public class Pet : Shared.Entity<PetId>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public PetInfo Info { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public string HealthInfo { get; private set; } = default!;
        public Address Address { get; private set; } = default!;
        public double Weight { get; private set; } = default;
        public double Height { get; private set; } = default;
        public PhoneNumber PhoneNumber { get; private set; } = default!;
        public bool IsCastrated { get; private set; } = default;
        public bool IsVaccinated { get; private set; } = default;
        public DateTime? BirthDate { get; private set; } = default!;
        public AssistanceStatus Status { get; private set; } = default!;
        public RequisitesForHelpList RequisitesForHelp { get; private set; } = default!;
        public DateTime CreationDate { get; } = DateTime.Now;
        public PhotosList PetPhotos { get; private set; } = default!;


        private Pet(PetId id) : base(id) { }

        private Pet(
            PetId id, 
            string name,
            string description,
            PetInfo info,
            string color,
            string healthInfo,
            Address address,
            double weight,
            double height,
            PhoneNumber phoneNumber,
            bool isCastrated,
            bool isVaccinated,
            DateTime? birthDate,
            AssistanceStatus status,
            RequisitesForHelpList requisites) : base(id)
        {
            Name = name;
            Description = description;
            Info = info;
            Color = color;
            HealthInfo = healthInfo;
            Address = address;
            Weight = weight;
            Height = height;
            PhoneNumber = phoneNumber;
            IsCastrated = isCastrated;
            IsVaccinated = isVaccinated;
            BirthDate = birthDate;
            Status = status;
            RequisitesForHelp = requisites;
        }


        public static Result<Pet, Error> Create(
            PetId id, 
            string name, 
            string description,
            PetInfo info,
            string color,
            string healthInfo,
            Address address,
            double weight,
            double height,
            PhoneNumber phoneNumber,
            bool isCastrated,
            bool isVaccinated,
            DateTime? birthDate,
            AssistanceStatus status,
            RequisitesForHelpList requisites)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Errors.General.InvalidValue("Name");

            if(string.IsNullOrWhiteSpace(description))
                return Errors.General.InvalidValue("Description");

            if(string.IsNullOrWhiteSpace(color))
                return Errors.General.InvalidValue("Color");

            if(string.IsNullOrWhiteSpace(healthInfo))
                return Errors.General.InvalidValue("HealthInfo");

            if(weight <= 0)
                return Errors.General.InvalidValue("Weight");

            if(height <= 0)
                return Errors.General.InvalidValue("Height");

            return new Pet(
                id, 
                name, 
                description, 
                info, 
                color, 
                healthInfo, 
                address, 
                weight, 
                height, 
                phoneNumber, 
                isCastrated, 
                isVaccinated, 
                birthDate, 
                status, 
                requisites);
        }


        public static Result<Guid, Error> Update(
            Pet pet,
            string name,
            string description,
            PetInfo? info,
            string color,
            Address? address,
            double weight,
            double height,
            PhoneNumber? phoneNumber,
            bool isCastrated,
            bool isVaccinated,
            DateTime? birthDate,
            AssistanceStatus? status,
            RequisitesForHelpList requisites)
        {
            if(pet == null)
                return Errors.General.NullValue("Pet");

            if(!string.IsNullOrWhiteSpace(name))
                pet.Name = name;

            if(!string.IsNullOrWhiteSpace(description))
                pet.Description = description;

            if(info != null)
                pet.Info = info;

            if(!string.IsNullOrWhiteSpace(color))
                pet.Color = color;

            if(address != null)
                pet.Address = address;

            if(weight > 0)
                pet.Weight = weight;

            if(height > 0)
                pet.Height = height;

            if(phoneNumber != null)
                pet.PhoneNumber = phoneNumber;

            pet.IsCastrated = isCastrated;

            pet.IsVaccinated = isVaccinated;

            if(birthDate != null)
                pet.BirthDate = birthDate;

            if(status != null)
                pet.Status = (AssistanceStatus)status;

            if(requisites != null)
                pet.RequisitesForHelp = requisites;

            return pet.Id.Value;
        }
    }
}