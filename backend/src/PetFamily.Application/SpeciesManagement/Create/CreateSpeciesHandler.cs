using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement;
using PetFamily.Domain.SpeciesManagement.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.SpeciesManagement.Create
{
    public class CreateSpeciesHandler : ICommandHandler<Guid, CreateSpeciesCommand>
    {
        private readonly ISpeciesRepository _repository;
        private readonly IValidator<CreateSpeciesCommand> _validator;
        private readonly ILogger<CreateSpeciesHandler> _logger;

        public CreateSpeciesHandler(
            ISpeciesRepository repository,
            IValidator<CreateSpeciesCommand> validator,
            ILogger<CreateSpeciesHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            CreateSpeciesCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if(validationResult.IsValid == false)
                return validationResult.ToErrorList(_logger, "create", "species");

            var id = SpeciesId.NewId();
            var name = SpeciesName.Create(command.Name).Value;

            var species = new Species(id, name);

            await _repository.Add(species, cancellationToken);

            _logger.LogInformation("New record of species created with ID '{id}'", species.Id.Value);

            return species.Id.Value;
        }
    }
}
