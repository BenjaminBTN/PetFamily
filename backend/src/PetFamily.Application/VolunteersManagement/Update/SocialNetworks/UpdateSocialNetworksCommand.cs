using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Domain.VolunteersManagement.VO;
using System.Collections.Generic;

namespace PetFamily.Application.VolunteersManagement.Update.SocialNetworks
{
    public record UpdateSocialNetworksCommand(VolunteerId VolunteerId, List<SocialNetworkDto> SocialNetworkDtos);
}
