using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Application.VolunteersManagement.Update.Requsites;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record UpdateRequsitesRequest(List<RequsiteDto> RequsiteDtos)
    {
        public UpdateRequsitesCommand ToCommand(Guid id)
        {
            var volunteerId = VolunteerId.Create(id);
            return new(volunteerId, RequsiteDtos);
        }
    }
}
