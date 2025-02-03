using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Application.VolunteersManagement.Update.MainInfo;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record UpdateMainInfoRequest(
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
            var fullName = new FullNameDto(
                Name,
                Surname,
                Patronymic);

            return new UpdateMainInfoCommand(id, fullName, Description, Email, Experience, PhoneNumber);
        }
    }
}
