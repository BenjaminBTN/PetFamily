using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement.Enums;
using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.VolunteersManagement.Entities;

public class Pet : Shared.Entity<PetId>, IDeletable
{
    private bool _isDeleted = false;

    public PetName Name { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public PetType TypeInfo { get; private set; } = default!;
    public PetColor Color { get; private set; } = default!;
    public PetHealthInfo? HealthInfo { get; private set; } = default!;
    public Address? Address { get; private set; } = default!;
    public double Weight { get; private set; } = default;
    public double Height { get; private set; } = default;
    public PhoneNumber PhoneNumber { get; private set; } = default!;
    public bool IsCastrated { get; private set; } = default;
    public bool IsVaccinated { get; private set; } = default;
    public DateTime? BirthDate { get; private set; } = default!;
    public AssistanceStatus Status { get; private set; } = default!;
    public OrdinalNumber OrdinalNumber { get; private set; } = default!;

    public DateTime CreationDate { get; } = DateTime.Now.ToLocalTime();

    public RequisiteForHelpList RequisitesForHelp { get; private set; } = new();
    public PhotoList PetPhotos { get; private set; } = new();


    private Pet(PetId id) : base(id) { }

    private Pet(
        PetId id,
        PetName name,
        Description description,
        PetType typeInfo,
        PetColor color,
        PetHealthInfo? healthInfo,
        Address? address,
        double weight,
        double height,
        PhoneNumber phoneNumber,
        bool isCastrated,
        bool isVaccinated,
        DateTime? birthDate,
        AssistanceStatus status) : base(id)
    {
        Name = name;
        Description = description;
        TypeInfo = typeInfo;
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
    }


    public static Result<Pet, Error> Create(
        PetId id,
        PetName name,
        Description description,
        PetType typeInfo,
        PetColor color,
        PetHealthInfo? healthInfo,
        Address? address,
        double weight,
        double height,
        PhoneNumber phoneNumber,
        bool isCastrated,
        bool isVaccinated,
        DateTime? birthDate,
        AssistanceStatus status)
    {
        if(weight <= 0)
            return Errors.General.InvalidValue("Weight");

        if(height <= 0)
            return Errors.General.InvalidValue("Height");

        return new Pet(
            id,
            name,
            description,
            typeInfo,
            color,
            healthInfo,
            address,
            weight,
            height,
            phoneNumber,
            isCastrated,
            isVaccinated,
            birthDate,
            status);
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


    public void SetOrdinalNumber(OrdinalNumber ordinalNumber) => 
        OrdinalNumber = ordinalNumber;


    public UnitResult<Error> MoveForward()
    {
        var result = OrdinalNumber.Forward();
        if(result.IsFailure)
            return result.Error;

        OrdinalNumber = result.Value;

        return UnitResult.Success<Error>();
    }


    public UnitResult<Error> MoveBack()
    {
        var result = OrdinalNumber.Back();
        if(result.IsFailure)
            return result.Error;

        OrdinalNumber = result.Value;

        return UnitResult.Success<Error>();
    }


    public void UpdateMainInfo(
        PetName name,
        Description description,
        PetType typeInfo,
        PetColor color,
        PetHealthInfo? healthInfo,
        Address? address,
        double weight,
        double height,
        PhoneNumber phoneNumber,
        bool isCastrated,
        bool isVaccinated,
        DateTime? birthDate,
        AssistanceStatus status)
    {
        Name = name;
        Description = description;
        TypeInfo = typeInfo;
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
    }


    public void UpdateRequisitesForHelp(RequisiteForHelpList requisites)
    {
        RequisitesForHelp = requisites;
    }


    public void UpdatePetPhotos(IEnumerable<FilePath> paths)
    {
        List<Photo> photos = new List<Photo>();
        paths.ToList().ForEach(path => photos.Add(new Photo(path)));

        PetPhotos = new PhotoList(photos);
    }
}
