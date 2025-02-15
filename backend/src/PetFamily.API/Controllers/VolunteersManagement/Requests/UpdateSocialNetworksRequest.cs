using PetFamily.Application.VolunteersManagement.Commands.Update.SocialNetworks;
using PetFamily.Application.VolunteersManagement.Dtos;

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
