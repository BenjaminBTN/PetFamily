using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Application.VolunteersManagement.Update.SocialNetworks;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record UpdateSocialNetworksRequest(List<SocialNetworkDto> SocialNetworksDto)
    {
        public UpdateSocialNetworksCommand ToCommand(Guid id)
        {
            return new(id, SocialNetworksDto);
        }
    }
}
