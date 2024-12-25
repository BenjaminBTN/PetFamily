using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.MainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IVolunteersRepository _repository;
        private readonly IValidator<UpdateMainInfoCommand> _validator;
        private readonly ILogger<UpdateMainInfoCommand> _logger;

        public UpdateMainInfoHandler(
            IVolunteersRepository repository,
            IValidator<UpdateMainInfoCommand> validator,
            ILogger<UpdateMainInfoCommand> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(UpdateMainInfoCommand command, CancellationToken cancellationToken)
        {
            // validation
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if(validationResult.IsValid == false)
            {
                var validationErrors = validationResult.Errors;
                List<Error> errors = [];

                foreach(var validationError in validationErrors)
                {
                    var error = Error.Deserialise(validationError.ErrorMessage);
                    errors.Add(Error.Validation(error.Code, error.Message, validationError.PropertyName));

                    _logger.LogError("Can not update volunteer record: {errorMessage}", error.Message);

                    return new ErrorList(errors);
                }
            }

            // try getting an entity
            var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);
            if(volunteerResult.IsFailure)
                return volunteerResult.Error;

            var volunteer = volunteerResult.Value;

            // create VOs
            var fullName = FullName.Create(
                command.FullNameDto.Name,
                command.FullNameDto.Surname,
                command.FullNameDto.Patronymic).Value;

            var description = Description.Create(command.Description).Value;

            var email = Email.Create(command.Email).Value;

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

            // update
            volunteer.UpdateMainInfo(
                fullName,
                description,
                email,
                command.Experience,
                phoneNumber);

            await _repository.Save(volunteer, cancellationToken);

            _logger.LogInformation("An existing volunteer record with ID: {id} has been successfully updated",
                volunteer.Id.Value);

            return volunteer.Id.Value;
        }
    }
}
