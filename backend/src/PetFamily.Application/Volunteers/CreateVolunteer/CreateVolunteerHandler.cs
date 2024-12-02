using CSharpFunctionalExtensions;
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

        public CreateVolunteerHandler(IVolunteersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid, Error>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken = default)
        {
            var id = VolunteerId.NewId();

            var fullNameResult = FullName.Create(command.FullName.Name, command.FullName.Surname, command.FullName.Patronymic);
            if(fullNameResult.IsFailure)
                return fullNameResult.Error;

            var emailResult = Email.Create(command.Email);
            if(emailResult.IsFailure)
                return emailResult.Error;

            var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
            if(phoneNumberResult.IsFailure)
                return phoneNumberResult.Error;

            var volunteer = Volunteer.Create(id, fullNameResult.Value, command.Description, emailResult.Value, 
                command.Experience, phoneNumberResult.Value).Value;

            await _repository.Add(volunteer, cancellationToken);

            return volunteer.Id.Value;
        }
    }
}
