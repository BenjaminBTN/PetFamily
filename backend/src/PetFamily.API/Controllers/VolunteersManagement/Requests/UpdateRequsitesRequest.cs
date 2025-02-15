using PetFamily.Application.VolunteersManagement.Commands.Update.Requsites;
using PetFamily.Application.VolunteersManagement.Dtos;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record UpdateRequsitesRequest(List<RequsiteDto> RequsitesDto)
    {
        public UpdateRequsitesCommand ToCommand(Guid id)
        {
            return new(id, RequsitesDto);
        }
    }
}
