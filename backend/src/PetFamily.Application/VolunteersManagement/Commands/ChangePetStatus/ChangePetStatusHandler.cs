using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.SpeciesManagement;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.SpeciesManagement.VO;
using PetFamily.Domain.VolunteersManagement.Enums;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.VolunteersManagement.Commands.ChangePetStatus;

public class ChangePetStatusHandler : ICommandHandler<Guid, ChangePetStatusCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ChangePetStatusCommand> _validator;
    private readonly ILogger<ChangePetStatusHandler> _logger;

    public ChangePetStatusHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<ChangePetStatusCommand> validator,
        ILogger<ChangePetStatusHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        ChangePetStatusCommand command,
        CancellationToken ct)
    {
        // validation
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList(_logger, "update", "pet status");

        // get a volunteer
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteerResult = await _volunteersRepository.GetById(volunteerId, ct);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        // get a pet from a volunteer
        var petId = PetId.Create(command.PetId);
        var petResult = volunteer.GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        var pet = petResult.Value;

        // update pet status
        var status = (AssistanceStatus)command.Status;

        pet.ChangeStatus(status);

        await _unitOfWork.SaveChanges(ct);

        _logger.LogInformation("The status of a pet record with ID '{id}' has been successfully updated",
            pet.Id.Value);

        return pet.Id.Value;
    }
}
