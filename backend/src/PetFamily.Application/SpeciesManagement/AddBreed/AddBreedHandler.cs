using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Entities;
using PetFamily.Domain.SpeciesManagement.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.SpeciesManagement.AddBreed
{
    public class AddBreedHandler
    {
        private readonly ISpeciesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddBreedCommand> _validator;
        private readonly ILogger<AddBreedHandler> _logger;

        public AddBreedHandler(
            ISpeciesRepository repository,
            IUnitOfWork unitOfWork,
            IValidator<AddBreedCommand> validator,
            ILogger<AddBreedHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            AddBreedCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if(validationResult.IsValid == false)
                return validationResult.ToErrorList(_logger, "add", "breed");

            var speciesId = SpeciesId.Create(command.SpeciesId);

            var speciesResult = await _repository.GetById(speciesId, cancellationToken);
            if(speciesResult.IsFailure)
            {
                _logger.LogError("The species record with ID '{id}' was not found: {errorMessage}",
                    speciesId, speciesResult.Error.Message);
                return speciesResult.Error.ToErrorList();
            }

            var species = speciesResult.Value;
            var id = BreedId.NewId();
            var name = BreedName.Create(command.Name).Value;
            var newBreed = new Breed(id, name);

            species.AddBreed(newBreed);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation(
                "New record of breed created with ID '{id}' for species '{spesiesName}'", 
                newBreed.Id.Value, species.Name.Value);

            return species.Id.Value;
        }
    }
}
