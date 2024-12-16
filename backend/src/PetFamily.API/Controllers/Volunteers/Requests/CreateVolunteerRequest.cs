using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers.Dtos;

namespace PetFamily.API.Controllers.Volunteers.Requests
{
    public record CreateVolunteerRequest(
        string Name, 
        string Surname, 
        string Patronymic, 
        string Description, 
        string Email, 
        int Experience, 
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
    };
}
