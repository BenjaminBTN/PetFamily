using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Application.VolunteersManagement.Update.Requsites;

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
