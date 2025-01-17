using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Pets.Create
{


    public class CreatePetHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger _logger;

        public CreatePetHandler(IFileProvider fileProvider, ILogger logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string, Error>> Handle()
        {
            var result = await _fileProvider.Upload();
            if(result.IsFailure)
                return result.Error;

            _logger.LogInformation("New record of pet created with ID: {id}", Guid.NewGuid());

            return result.Value;
        }
    }
}
