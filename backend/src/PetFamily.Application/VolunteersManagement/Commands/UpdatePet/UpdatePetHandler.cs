using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.SpeciesManagement;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.SpeciesManagement.VO;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.VolunteersManagement.Commands.UpdatePet;

public class UpdatePetHandler : ICommandHandler<Guid, UpdatePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetCommand> _validator;
    private readonly ILogger<UpdatePetHandler> _logger;

    public UpdatePetHandler(
        IVolunteersRepository volunteersRepository,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdatePetCommand> validator,
        ILogger<UpdatePetHandler> logger,
        IReadDbContext readDbContext)
    {
        _volunteersRepository = volunteersRepository;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
        _readDbContext = readDbContext;

    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePetCommand command,
        CancellationToken ct)
    {
        // validation
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList(_logger, "update", "pet");

        // search species by name
        var speciesName = command.TypeInfo.Species;

        var speciesDto = _readDbContext.Species.SingleOrDefault(s => s.Name == speciesName);
        if (speciesDto == null)
        {
            _logger.LogError("The Species record with name '{name}' was not found", speciesName);
            return Errors.General.NotFound("Species").ToErrorList();
        }

        // search breed by name
        var breedName = command.TypeInfo.Breed;

        var breedDto = _readDbContext.Breeds
            .SingleOrDefault(s => s.Name == breedName && s.SpeciesId == speciesDto.Id);

        if (breedDto == null)
        {
            _logger.LogError(
                "The Breed record with name '{breedName}' was not found for Species '{speciesName}'",
                breedName, speciesName);
            return Errors.General.NotFound("Breed").ToErrorList();
        }

        // get a volunteer
        var volunteerId = VolunteerId.Create(command.Id);
        var volunteerResult = await _volunteersRepository.GetById(volunteerId, ct);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        // get a pet from a volunteer
        var petId = PetId.Create(command.PetId);
        var petResult = volunteer.GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        var pet = petResult.Value;

        // update pet
        var name = PetName.Create(command.Name.ToLower()).Value;
        var description = Description.Create(command.Description).Value;
        var typeInfo = PetType.Create(SpeciesId.Create(speciesDto.Id), breedDto.Id).Value;
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

        var birthDateResult = NullableDateTimeParser.CreateDateTime(command.BirthDate);
        if (birthDateResult.IsFailure)
        {
            _logger.LogError("Invalid date time format");
            return birthDateResult.Error.ToErrorList();
        }
        var birthDate = birthDateResult.Value;

        var requisites = new RequisiteForHelpList(
            command.RequisitesForHelp.Select(dto => RequisiteForHelp.Create(dto.Name, dto.Description).Value));

        pet.UpdateMainInfo(name, description, typeInfo, color, healthInfo, address, command.Weight, command.Height, phoneNumber, command.IsCastrated, command.IsVaccinated, birthDate, requisites);

        await _unitOfWork.SaveChanges(ct);

        _logger.LogInformation("The pet record with ID '{id}' has been successfully updated",
            pet.Id.Value);

        return pet.Id.Value;
    }
}
