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
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.VolunteersManagement.Commands.HardDeletePet;

public class HardDeletePetHandler : ICommandHandler<Guid, HardDeletePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileProvider _fileProvider;
    private readonly IValidator<HardDeletePetCommand> _validator;
    private readonly ILogger<HardDeletePetHandler> _logger;

    public HardDeletePetHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IFileProvider fileProvider,
        IValidator<HardDeletePetCommand> validator,
        ILogger<HardDeletePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _fileProvider = fileProvider;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        HardDeletePetCommand command,
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

        // hard-delete pet
        volunteer.DeletePet(pet);

        var filesToDelete = pet.PetPhotos.Photos.Select(p => new FileInfo(p.PathToStorage.Value, Buckets.PHOTOS));

        var deleteResult = await _fileProvider.Delete(filesToDelete, ct);
        if (deleteResult.IsFailure)
            return deleteResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(ct);

        _logger.LogInformation("An existing pet record with ID '{id}' has been successfully hard deleted",
            pet.Id.Value);

        return pet.Id.Value;
    }
}
