using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.VO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteersRepository _repository;
        private readonly IValidator<CreateVolunteerCommand> _validator;
        private readonly ILogger<CreateVolunteerCommand> _logger;

        public CreateVolunteerHandler(
            IVolunteersRepository repository,
            IValidator<CreateVolunteerCommand> validator,
            ILogger<CreateVolunteerCommand> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if(validationResult.IsValid == false)
            {
                var validationErrors = validationResult.Errors;
                List<Error> errors = [];

                foreach(var validationError in validationErrors)
                {
                    var error = Error.Deserialise(validationError.ErrorMessage);
                    errors.Add(Error.Validation(error.Code, error.Message, validationError.PropertyName));

                    return new ErrorList(errors);
                }
            }


            var id = VolunteerId.NewId();

            var fullName = FullName.Create(
                command.FullNameDto.Name, 
                command.FullNameDto.Surname, 
                command.FullNameDto.Patronymic).Value;

            var email = Email.Create(command.Email).Value;

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

            var volunteer = Volunteer.Create(
                id, 
                fullName, 
                command.Description, 
                email, 
                command.Experience, 
                phoneNumber).Value;

            await _repository.Add(volunteer, cancellationToken);

            _logger.LogInformation("New record of volunteer created with ID: {id}", volunteer.Id.Value);

            return volunteer.Id.Value;
        }
    }
}
