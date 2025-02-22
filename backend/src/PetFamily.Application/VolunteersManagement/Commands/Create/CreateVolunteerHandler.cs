using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement;
using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Commands.Create
{
    public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
    {
        private readonly IVolunteersRepository _repository;
        private readonly IValidator<CreateVolunteerCommand> _validator;
        private readonly ILogger<CreateVolunteerHandler> _logger;

        public CreateVolunteerHandler(
            IVolunteersRepository repository,
            IValidator<CreateVolunteerCommand> validator,
            ILogger<CreateVolunteerHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            CreateVolunteerCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if(validationResult.IsValid == false)
                return validationResult.ToErrorList(_logger, "create", "volunteer");

            var id = VolunteerId.NewId();

            var fullName = FullName.Create(
                command.FullNameDto.Name,
                command.FullNameDto.Surname,
                command.FullNameDto.Patronymic).Value;

            var description = Description.Create(command.Description).Value;

            var email = Email.Create(command.Email).Value;

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

            var volunteer = Volunteer.Create(
                id,
                fullName,
                description,
                email,
                command.Experience,
                phoneNumber).Value;

            await _repository.Add(volunteer, cancellationToken);

            _logger.LogInformation("New record of volunteer created with ID '{id}'", volunteer.Id.Value);

            return volunteer.Id.Value;
        }
    }
}
