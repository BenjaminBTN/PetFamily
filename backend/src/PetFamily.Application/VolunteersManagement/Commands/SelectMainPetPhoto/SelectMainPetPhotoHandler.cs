using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.VolunteersManagement.Commands.SelectMainPetPhoto;

public class SelectMainPetPhotoHandler : ICommandHandler<SelectMainPetPhotoCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SelectMainPetPhotoCommand> _validator;
    private readonly ILogger<SelectMainPetPhotoHandler> _logger;

    public SelectMainPetPhotoHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<SelectMainPetPhotoCommand> validator,
        ILogger<SelectMainPetPhotoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        SelectMainPetPhotoCommand command,
        CancellationToken ct)
    {
        // validation
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList(_logger, "update", "pet");

        // get a volunteer
        var volunteerId = VolunteerId.Create(command.Id);
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

        // select main photo
        var paths = pet.PetPhotos.Photos.Select(p => p.PathToStorage).ToList();

        try
        {
            var path = paths.Single(p => p.Value == command.FilePath);
            paths.Remove(path);
        }
        catch (Exception)
        {
            _logger.LogError("Pet photo with name '{name}' not found", command.FilePath);
            return Errors.General.NotFound("Pet photo with name: " + command.FilePath).ToErrorList();
        }

        var newPhotoList = new List<Photo>();
        var mainPhoto = new Photo(FilePath.Create(command.FilePath).Value, true);
        
        newPhotoList.Add(mainPhoto);
        newPhotoList.AddRange(paths.Select(path => new Photo(path, false)));

        pet.UpdatePetPhotos(newPhotoList);

        await _unitOfWork.SaveChanges(ct);

        _logger.LogInformation("The main photo of the pet record with ID '{id}' has been successfully changed",
            pet.Id.Value);

        return UnitResult.Success<ErrorList>();
    }
}
