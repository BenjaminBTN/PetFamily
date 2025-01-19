using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.AddPet
{
    public class AddPetHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<AddPetHandler> _logger;

        public AddPetHandler(
            IVolunteersRepository volunteersRepository, 
            IFileProvider fileProvider, 
            ILogger<AddPetHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string, Error>> Handle(
            AddPetCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _fileProvider.Upload(command.FileDto, cancellationToken);
            if(result.IsFailure)
                return result.Error;

            _logger.LogInformation("New record of pet created with ID: {id}", Guid.NewGuid());

            return result.Value;
        }
    }
}
