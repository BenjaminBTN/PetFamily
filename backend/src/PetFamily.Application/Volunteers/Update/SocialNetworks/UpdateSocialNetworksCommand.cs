using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain.Volunteers.VO;
using System.Collections.Generic;

namespace PetFamily.Application.Volunteers.Update.SocialNetworks
{
    public record UpdateSocialNetworksCommand(VolunteerId VolunteerId, List<SocialNetworkDto> SocialNetworkDtos);
}
