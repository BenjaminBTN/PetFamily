﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.SpeciesManagement;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.SpeciesManagement.VO;
using PetFamily.Domain.VolunteersManagement.Entities;
using PetFamily.Domain.VolunteersManagement.Enums;
using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Commands.AddPet;

public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetHandler(
        IVolunteersRepository volunteersRepository,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<AddPetCommand> validator,
        ILogger<AddPetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken)
    {
        // validation
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if(validationResult.IsValid == false)
            return validationResult.ToErrorList(_logger, "add", "pet");

        // search species by name
        var speciesNameString = command.TypeInfo.Species;
        var speciesName = SpeciesName.Create(speciesNameString.ToLower()).Value;

        var speciesResult = await _speciesRepository.GetByName(speciesName, cancellationToken);
        if(speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var species = speciesResult.Value;

        // search breed by name
        var breedNameString = command.TypeInfo.Breed;
        var breedName = BreedName.Create(breedNameString.ToLower()).Value;

        var breedResult = species.GetBreedByName(breedName);
        if(breedResult.IsFailure)
        {
            _logger.LogError("The breed record with name '{name}' was not found: {errorMessage}",
                breedName.Value, breedResult.Error.Message);
            return breedResult.Error.ToErrorList();
        }

        var breed = breedResult.Value;

        // get volunteer
        var volunteerId = VolunteerId.Create(command.Id);
        var volunteerResult = await _volunteersRepository.GetById(volunteerId, cancellationToken);
        if(volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        // add pet to volunteer
        var petId = PetId.NewId();
        var name = PetName.Create(command.Name.ToLower()).Value;
        var description = Description.Create(command.Description).Value;

        var typeInfo = PetType.Create(species.Id, breed.Id.Value).Value;

        var color = PetColor.Create(command.Color).Value;
        var healthInfo = PetHealthInfo.Create(command.HealthInfo).Value;
        var address = Address.Create(
            command.Address.Country.ToLower(),
            command.Address.Region.ToLower(),
            command.Address.City.ToLower(),
            command.Address.Street.ToLower(),
            command.Address.HouseNumber,
            command.Address.PostalCode.ToLower()).Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var birthDateResult = CreateBirthDate(command.BirthDate);
        if(birthDateResult.IsFailure)
            return birthDateResult.Error.ToErrorList();

        var status = (AssistanceStatus)command.Status;

        var pet = Pet.Create(petId, name, description, typeInfo, color,
            healthInfo, address, command.Weight, command.Height, phoneNumber,
            command.IsCastrated, command.IsVaccinated, birthDateResult.Value, status).Value;

        var addPetResult = volunteer.AddPet(pet);
        if(addPetResult.IsFailure)
            return addPetResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("New record of a pet created with ID '{id}' and added to a volunteer",
            pet.Id.Value);

        return pet.Id.Value;
    }

    private Result<DateTime?, Error> CreateBirthDate(string? value)
    {
        DateTime? birthDate;
        if(value == null)
            birthDate = null;
        else
        {
            try
            {
                birthDate = DateTime.Parse(value).ToLocalTime();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Invalid date time format");
                return Errors.General.InvalidValue("Birth date");
            }
        }

        return birthDate;
    }
}
