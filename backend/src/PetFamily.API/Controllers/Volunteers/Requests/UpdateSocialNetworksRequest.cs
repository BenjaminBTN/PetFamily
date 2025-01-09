using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Application.Volunteers.Update.SocialNetworks;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.API.Controllers.Volunteers.Requests
{
    public record UpdateSocialNetworksRequest(List<SocialNetworkDto> SocialNetworkDtos)
    {
        public UpdateSocialNetworksCommand ToCommand(Guid id)
        {
            var volunteerId = VolunteerId.Create(id);
            return new(volunteerId, SocialNetworkDtos);
        }
    }
}
