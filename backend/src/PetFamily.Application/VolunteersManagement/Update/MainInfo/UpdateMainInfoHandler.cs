using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Update.MainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IVolunteersRepository _repository;
        private readonly IValidator<UpdateMainInfoCommand> _validator;
        private readonly ILogger<UpdateMainInfoHandler> _logger;

        public UpdateMainInfoHandler(
            IVolunteersRepository repository,
            IValidator<UpdateMainInfoCommand> validator,
            ILogger<UpdateMainInfoHandler> logger)
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
                return validationResult.ToErrorList(_logger, "update", "volunteer");

            // try getting an entity
            var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);
            if(volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

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
