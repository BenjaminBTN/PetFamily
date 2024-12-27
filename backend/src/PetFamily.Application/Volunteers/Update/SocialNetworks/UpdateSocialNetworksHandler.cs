using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.SocialNetworks
{
    public class UpdateSocialNetworksHandler
    {
        private readonly IVolunteersRepository _repository;
        private readonly IValidator<UpdateSocialNetworksCommand> _validator;
        private readonly ILogger<UpdateSocialNetworksHandler> _logger;

        public UpdateSocialNetworksHandler(
            IVolunteersRepository repository,
            IValidator<UpdateSocialNetworksCommand> validator,
            ILogger<UpdateSocialNetworksHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(UpdateSocialNetworksCommand command, CancellationToken cancellationToken)
        {
            // validation
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if(validationResult.IsValid == false)
            {
                var validationErrors = validationResult.Errors;
                List<Error> errors = [];

                foreach(var validationError in validationErrors)
                {
                    var error = Error.Deserialise(validationError.ErrorMessage);
                    errors.Add(Error.Validation(error.Code, error.Message, validationError.PropertyName));

                    _logger.LogError("Can not update volunteer record: {errorMessage}", error.Message);

                    return new ErrorList(errors);
                }
            }

            // try getting an entity
            var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);
            if(volunteerResult.IsFailure)
                return volunteerResult.Error;

            var volunteer = volunteerResult.Value;

            // create VO
            List<SocialNetwork> list = [];

            foreach(SocialNetworkDto dto in command.SocialNetworkDtos)
            {
                var network = SocialNetwork.Create(dto.Name, dto.Url).Value;
                list.Add(network);
            }

            var socialNetworkList = new SocialNetworkList(list);

            // update
            volunteer.UpdateSocialNetworks(socialNetworkList);

            await _repository.Save(volunteer, cancellationToken);

            _logger.LogInformation("An existing volunteer record with ID: {id} has been successfully updated",
                volunteer.Id.Value);

            return volunteer.Id.Value;
        }
    }
}