using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteersRepository _repository;
        private readonly IValidator<CreateVolunteerCommand> _validator;

        public CreateVolunteerHandler(IVolunteersRepository repository, IValidator<CreateVolunteerCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<Guid, Error>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            // доработать после введения Envelope
            if(validationResult.IsValid == false)
                return Errors.General.InvalidValue(validationResult.Errors[0].PropertyName);

            var id = VolunteerId.NewId();

            var fullName = FullName.Create(command.FullName.Name, command.FullName.Surname, command.FullName.Patronymic).Value;

            var email = Email.Create(command.Email).Value;

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

            var volunteer = Volunteer.Create(id, fullName, command.Description, email, 
                command.Experience, phoneNumber).Value;

            return await _repository.Add(volunteer, cancellationToken);
        }
    }
}
