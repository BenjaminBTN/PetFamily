using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers.VO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.Requsites
{
    public class UpdateRequsitesHandler
    {
        private readonly IVolunteersRepository _repository;
        private readonly IValidator<UpdateRequsitesCommand> _validator;
        private readonly ILogger<UpdateRequsitesCommand> _logger;

        public UpdateRequsitesHandler(
            IVolunteersRepository repository,
            IValidator<UpdateRequsitesCommand> validator,
            ILogger<UpdateRequsitesCommand> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(UpdateRequsitesCommand command, CancellationToken cancellationToken)
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
            List<VolunteerRequisite> list = [];

            foreach(RequsiteDto dto in command.RequsiteDtos)
            {
                var requisite = VolunteerRequisite.Create(dto.Name, dto.Description).Value;
                list.Add(requisite);
            }

            var requisiteList = new VolunteerRequisiteList(list);

            // update
            volunteer.UpdateRequisites(requisiteList);

            await _repository.Save(volunteer, cancellationToken);

            _logger.LogInformation("An existing volunteer record with ID: {id} has been successfully updated",
                volunteer.Id.Value);

            return volunteer.Id.Value;
        }
    }
}