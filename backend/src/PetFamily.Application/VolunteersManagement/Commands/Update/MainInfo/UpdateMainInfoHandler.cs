using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Commands.Update.MainInfo;

public class UpdateMainInfoHandler : ICommandHandler<Guid, UpdateMainInfoCommand>
{
    private readonly IVolunteersRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateMainInfoCommand> _validator;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(
        IVolunteersRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateMainInfoCommand> validator,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
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
        var volunteerId = VolunteerId.Create(command.VolunteerId);

        var volunteerResult = await _repository.GetById(volunteerId, cancellationToken);
        if(volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        // create VOs
        var fullName = FullName.Create(
            command.FullNameDto.Name.ToLower(),
            command.FullNameDto.Surname.ToLower(),
            command.FullNameDto.Patronymic.ToLower()).Value;

        var description = Description.Create(command.Description).Value;

        var email = Email.Create(command.Email.ToLower()).Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        // update
        volunteer.UpdateMainInfo(
            fullName,
            description,
            email,
            command.Experience,
            phoneNumber);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "An existing volunteer record with ID '{id}' has been successfully updated",
            volunteer.Id.Value);

        return volunteer.Id.Value;
    }
}
