using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.AddPet
{
    public class AddPetHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ILogger<AddPetHandler> _logger;

        public AddPetHandler(
            IVolunteersRepository volunteersRepository,
            ILogger<AddPetHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> Handle(
            AddPetCommand command,
            CancellationToken cancellationToken)
        {


            _logger.LogInformation("New record of pet created with ID: {id}", Guid.NewGuid());

            return "";
        }
    }
}
