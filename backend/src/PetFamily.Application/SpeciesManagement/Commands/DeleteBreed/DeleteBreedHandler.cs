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
using PetFamily.Domain.SpeciesManagement.VO;

namespace PetFamily.Application.SpeciesManagement.Commands.DeleteBreed;

public class DeleteBreedHandler : ICommandHandler<DeleteBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IReadDbContext _readDbContext;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteBreedCommand> _validator;
    public readonly ILogger<DeleteBreedHandler> _logger;

    public DeleteBreedHandler(ISpeciesRepository speciesRepository, IReadDbContext readDbContext, IValidator<DeleteBreedCommand> validator, ILogger<DeleteBreedHandler> logger, IUnitOfWork unitOfWork)
    {
        _speciesRepository = speciesRepository;
        _readDbContext = readDbContext;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;

    }

    public async Task<UnitResult<ErrorList>> Handle(DeleteBreedCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList(_logger, "delete", "breed");

        var petsQuery = _readDbContext.PetsWithSpeciesAndBreed;

        var petResult = petsQuery.FirstOrDefault(pet => pet.BreedId == command.BreedId);
        if (petResult != null)
            return Error.Conflict("property.is.necessary", "The 'Breed' continue to be used").ToErrorList();

        var speciesId = SpeciesId.Create(command.SpeciesId);
        var speciesResult = await _speciesRepository.GetById(speciesId, ct);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedId = BreedId.Create(command.BreedId);
        var breedResult = speciesResult.Value.GetBreedById(breedId);
        if (breedResult.IsFailure)
            return breedResult.Error.ToErrorList();

        speciesResult.Value.DeleteBreed(breedResult.Value);

        await _unitOfWork.SaveChanges(ct);

        _logger.LogInformation(
            "The Breed '{breedName}' has been successfully deleted from the Species '{speciesName}'",
            breedResult.Value.Name.Value, speciesResult.Value.Name.Value);

        return UnitResult.Success<ErrorList>();
    }
}
