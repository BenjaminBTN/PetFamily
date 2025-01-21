using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.SoftDelete
{
    public class SoftDeleteVolunteerHandler
    {
        private readonly IVolunteersRepository _repository;
        private readonly IValidator<SoftDeleteVolunteerCommand> _validator;
        private readonly ILogger<SoftDeleteVolunteerHandler> _logger;

        public SoftDeleteVolunteerHandler(
            IVolunteersRepository repository,
            IValidator<SoftDeleteVolunteerCommand> validator,
            ILogger<SoftDeleteVolunteerHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(SoftDeleteVolunteerCommand command, CancellationToken cancellationToken)
        {
            // validation
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if(validationResult.IsValid == false)
                return validationResult.ToErrorList(_logger, "delete", "volunteer");

            // try getting an entity
            var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);
            if(volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var volunteer = volunteerResult.Value;

            // delete
            await _repository.SoftDelete(volunteer, cancellationToken);

            _logger.LogInformation("An existing volunteer record with ID: {id} has been successfully soft deleted",
                volunteer.Id.Value);

            return volunteer.Id.Value;
        }
    }
}
