using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Application.Volunteers.Update;
using PetFamily.Domain.Volunteers.VO;

namespace PetFamily.API.Controllers.Volunteers.Requests
{
    public record UpdateVolunteerRequest(
        string Name,
        string Surname,
        string Patronymic,
        string Description,
        string Email,
        double Experience,
        string PhoneNumber)
    {
        public UpdateMainInfoCommand ToCommand(Guid id)
        {
            VolunteerId volunteerId = VolunteerId.Create(id);

            var fullName = new FullNameDto(
                Name,
                Surname,
                Patronymic);

            return new UpdateMainInfoCommand(volunteerId, fullName, Description, Email, Experience, PhoneNumber);
        }
    }
}
