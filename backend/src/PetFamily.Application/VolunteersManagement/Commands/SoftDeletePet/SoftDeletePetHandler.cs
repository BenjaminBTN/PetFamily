using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.VolunteersManagement.Commands.SoftDeletePet;

public class SoftDeletePetHandler : ICommandHandler<Guid, SoftDeletePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SoftDeletePetCommand> _validator;
    private readonly ILogger<SoftDeletePetHandler> _logger;

    public SoftDeletePetHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<SoftDeletePetCommand> validator,
        ILogger<SoftDeletePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        SoftDeletePetCommand command,
        CancellationToken ct)
    {
        // validation
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList(_logger, "delete", "pet");

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

        // soft-delete pet
        pet.Delete();

        await _unitOfWork.SaveChanges(ct);

        _logger.LogInformation("The pet record with ID '{id}' has been successfully soft-deleted",
            pet.Id.Value);

        return pet.Id.Value;
    }
}
