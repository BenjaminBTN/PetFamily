using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
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

namespace PetFamily.Application.VolunteersManagement.AddPet
{
    public class AddPetHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IValidator<AddPetCommand> _validator;
        private readonly ILogger<AddPetHandler> _logger;

        public AddPetHandler(
            IVolunteersRepository volunteersRepository,
            ISpeciesRepository speciesRepository,
            IValidator<AddPetCommand> validator,
            ILogger<AddPetHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _speciesRepository = speciesRepository;
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
            var speciesName = SpeciesName.Create(speciesNameString[..1].ToUpper() 
                + speciesNameString[1..].ToLower()).Value;

            var speciesResult = await _speciesRepository.GetByName(speciesName, cancellationToken);
            if(speciesResult.IsFailure)
                return speciesResult.Error.ToErrorList();

            var species = speciesResult.Value;

            // search breed by name
            var breedNameString = command.TypeInfo.Breed;
            var breedName = BreedName.Create(breedNameString[..1].ToUpper()
                + breedNameString[1..].ToLower()).Value;

            var breedResult = species.GetBreedByName(breedName);
            if(breedResult.IsFailure)
                return breedResult.Error.ToErrorList();

            var breed = breedResult.Value;

            // get volunteer
            var volunteerId = VolunteerId.Create(command.Id);
            var volunteerResult = await _volunteersRepository.GetById(volunteerId, cancellationToken);
            if(volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var volunteer = volunteerResult.Value;

            // add pet to volunteer
            var petId = PetId.NewId();
            var name = PetName.Create(command.Name).Value;
            var description = Description.Create(command.Description).Value;

            var typeInfo = PetType.Create(species.Id, breed.Id.Value).Value;

            var color = PetColor.Create(command.Color).Value;
            var healthInfo = PetHealthInfo.Create(command.HealthInfo).Value;
            var address = Address.Create(
                command.Address.Country,
                command.Address.Region,
                command.Address.City,
                command.Address.Street,
                command.Address.HouseNumber,
                command.Address.PostalCode).Value;

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

            DateTime? birthDate = null;
            if(command.BirthDate != null)
                birthDate = DateTime.Parse(command.BirthDate).ToLocalTime();

            var status = (AssistanceStatus)command.Status;

            var pet = Pet.Create(petId, name, description, typeInfo, color, 
                healthInfo, address, command.Weight, command.Height, phoneNumber,
                command.IsCastrated, command.IsVaccinated, birthDate, status).Value;

            volunteer.AddPet(pet);

            await _volunteersRepository.Save(volunteer, cancellationToken);

            _logger.LogInformation("New record of a pet created with ID: {id} and added to a volunteer", 
                pet.Id.Value);

            return pet.Id.Value;
        }
    }
}
