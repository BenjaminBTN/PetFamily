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
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.VO;

namespace PetFamily.Application.SpeciesManagement.DeleteSpecies;

public class DeleteSpeciesHandler : ICommandHandler<DeleteSpeciesCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<DeleteSpeciesCommand> _validator;
    public readonly ILogger<DeleteSpeciesHandler> _logger;

    public DeleteSpeciesHandler(ISpeciesRepository speciesRepository, IReadDbContext readDbContext, IValidator<DeleteSpeciesCommand> validator, ILogger<DeleteSpeciesHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _readDbContext = readDbContext;
        _validator = validator;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeleteSpeciesCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList(_logger, "delete", "species");

        var petsQuery = _readDbContext.PetsWithSpeciesAndBreed;

        var petResult = petsQuery.FirstOrDefault(pet => pet.SpeciesId == command.Id);
        if (petResult != null)
            return Error.Conflict("property.is.necessary", "The 'Species' continue to be used").ToErrorList();

        var speciesId = SpeciesId.Create(command.Id);

        var speciesResult = await _speciesRepository.GetById(speciesId, ct);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        await _speciesRepository.Delete(speciesResult.Value, ct);

        _logger.LogInformation(
            "The Species '{speciesName}' has been successfully deleted",
            speciesResult.Value.Name.Value);

        return UnitResult.Success<ErrorList>();
    }
}
