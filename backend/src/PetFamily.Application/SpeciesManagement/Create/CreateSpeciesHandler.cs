using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.SpeciesManagement.Create
{
    public class CreateSpeciesHandler
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly ILogger<CreateSpeciesHandler> _logger;

        public CreateSpeciesHandler(ISpeciesRepository repository, ILogger<CreateSpeciesHandler> logger)
        {
            _speciesRepository = repository;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            CreateSpeciesCommand command, 
            CancellationToken cancellationToken)
        {

        }
    }
}
