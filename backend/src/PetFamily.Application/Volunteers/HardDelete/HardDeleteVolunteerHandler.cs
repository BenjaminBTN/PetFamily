﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.HardDelete
{
    public class HardDeleteVolunteerHandler
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
            {
                return validationResult.ToErrorList(_logger, "delete", "volunteer");
            }

            // try getting an entity
            var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);
            if(volunteerResult.IsFailure)
                return volunteerResult.Error;

            var volunteer = volunteerResult.Value;

            // delete
            await _repository.HardDelete(volunteer, cancellationToken);

            _logger.LogInformation(
                "An existing volunteer record with ID: {id} has been successfully deleted beyond recovery",
                volunteer.Id.Value);

            return volunteer.Id.Value;
        }
    }
}
