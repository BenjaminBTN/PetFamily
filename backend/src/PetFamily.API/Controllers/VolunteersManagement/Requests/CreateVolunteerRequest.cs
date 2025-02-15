using PetFamily.Application.VolunteersManagement.Commands.Create;
using PetFamily.Application.VolunteersManagement.Dtos;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record CreateVolunteerRequest(
        string Name,
        string Surname,
        string Patronymic,
        string Description,
        string Email,
        double Experience,
        string PhoneNumber)
    {
        public CreateVolunteerCommand ToCommand()
        {
            var fullName = new FullNameDto(
                Name,
                Surname,
                Patronymic);

            return new CreateVolunteerCommand(fullName, Description, Email, Experience, PhoneNumber);
        }
    }
}
