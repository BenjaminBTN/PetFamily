using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.Volunteers.Enums;
using PetFamily.Domain.Volunteers.VO;
using System;

namespace PetFamily.Domain.Volunteers.Entities
{
    public class Pet : Shared.Entity<PetId>, IDeletable
    {
        private bool _isDeleted = false;

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
        public RequisiteForHelpList RequisitesForHelp { get; private set; } = default!;
        public PhotoList PetPhotos { get; private set; } = default!;
        public DateTime CreationDate { get; } = DateTime.Now.ToLocalTime();


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
            RequisiteForHelpList requisites) : base(id)
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
            RequisiteForHelpList requisites)
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


        public void Delete()
        {
            if(_isDeleted == false)
                _isDeleted = true;
        }


        public void Restore()
        {
            if(_isDeleted == true)
                _isDeleted = false;
        }


        public void Update(
            string name,
            string description,
            PetInfo info,
            string color,
            Address address,
            double weight,
            double height,
            PhoneNumber phoneNumber,
            bool isCastrated,
            bool isVaccinated,
            DateTime birthDate,
            AssistanceStatus status)
        {
            Name = name;
            Description = description;
            Info = info;
            Color = color;
            Address = address;
            Weight = weight;
            Height = height;
            PhoneNumber = phoneNumber;
            IsCastrated = isCastrated;
            IsVaccinated = isVaccinated;
            BirthDate = birthDate;
            Status = status;
        }
    }
}
