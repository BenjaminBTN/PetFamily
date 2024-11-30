using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.VO;

namespace PetFamily.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteersRepository _repository;

        public CreateVolunteerHandler(IVolunteersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request, CancellationToken token)
        {
            var id = VolunteerId.NewId();

            var fullNameResult = FullName.Create(request.Name, request.Surname, request.Patronymic);
            if(fullNameResult.IsFailure)
                return fullNameResult.Error;

            var emailResult = Email.Create(request.Email);
            if(emailResult.IsFailure)
                return emailResult.Error;

            var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
            if(phoneNumberResult.IsFailure)
                return phoneNumberResult.Error;

            var volunteer = Volunteer.Create(id, fullNameResult.Value, request.Description, emailResult.Value, 
                request.Experience, phoneNumberResult.Value).Value;

            await _repository.Add(volunteer, token);

            return volunteer.Id.Value;
        }
    }
}