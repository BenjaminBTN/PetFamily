using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Commands.HardDelete
{
    public class HardDeleteVolunteerHandler : ICommandHandler<Guid, HardDeleteVolunteerCommand>
    {
        private readonly IVolunteersRepository _repository;
        private readonly IValidator<HardDeleteVolunteerCommand> _validator;
        private readonly ILogger<HardDeleteVolunteerHandler> _logger;

        public HardDeleteVolunteerHandler(
            IVolunteersRepository repository,
            IValidator<HardDeleteVolunteerCommand> validator,
            ILogger<HardDeleteVolunteerHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            HardDeleteVolunteerCommand command,
            CancellationToken cancellationToken)
        {
            // validation
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if(validationResult.IsValid == false)
                return validationResult.ToErrorList(_logger, "delete", "volunteer");

            // try getting an entity
            var volunteerId = VolunteerId.Create(command.VolunteerId);

            var volunteerResult = await _repository.GetById(volunteerId, cancellationToken);
            if(volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var volunteer = volunteerResult.Value;

            // delete
            await _repository.HardDelete(volunteer, cancellationToken);

            _logger.LogInformation(
                "An existing volunteer record with ID '{id}' has been successfully deleted beyond recovery",
                volunteer.Id.Value);

            return volunteer.Id.Value;
        }
    }
}
