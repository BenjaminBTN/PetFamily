using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Application.Volunteers.Update.Requsites;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.API.Controllers.Volunteers.Requests
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
