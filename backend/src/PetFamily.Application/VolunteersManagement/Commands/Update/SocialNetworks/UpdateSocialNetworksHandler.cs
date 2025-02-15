using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Commands.Update.SocialNetworks
{
    public class UpdateSocialNetworksHandler
    {
        private readonly IVolunteersRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateSocialNetworksCommand> _validator;
        private readonly ILogger<UpdateSocialNetworksHandler> _logger;

        public UpdateSocialNetworksHandler(
            IVolunteersRepository repository,
            IUnitOfWork unitOfWork,
            IValidator<UpdateSocialNetworksCommand> validator,
            ILogger<UpdateSocialNetworksHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            UpdateSocialNetworksCommand command,
            CancellationToken cancellationToken)
        {
            // validation
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if(validationResult.IsValid == false)
                return validationResult.ToErrorList(_logger, "update", "volunteer");

            // try getting an entity
            var volunteerId = VolunteerId.Create(command.VolunteerId);

            var volunteerResult = await _repository.GetById(volunteerId, cancellationToken);
            if(volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var volunteer = volunteerResult.Value;

            // create VO
            List<SocialNetwork> list = [];

            foreach(SocialNetworkDto dto in command.SocialNetworksDto)
            {
                var network = SocialNetwork.Create(dto.Name, dto.Url).Value;
                list.Add(network);
            }

            var socialNetworkList = new SocialNetworkList(list);

            // update
            volunteer.UpdateSocialNetworks(socialNetworkList);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation(
                "An existing volunteer record with ID '{id}' has been successfully updated",
                volunteer.Id.Value);

            return volunteer.Id.Value;
        }
    }
}
