using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteersManagement.Commands.Update.SocialNetworks;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests;

public record UpdateSocialNetworksRequest(List<SocialNetworkDto> SocialNetworksDto)
{
    public UpdateSocialNetworksCommand ToCommand(Guid id)
    {
        return new(id, SocialNetworksDto);
    }
}
