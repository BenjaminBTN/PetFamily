using PetFamily.Application.VolunteersManagement.Dtos;
using System;
using System.Collections.Generic;

namespace PetFamily.Application.VolunteersManagement.Commands.Update.SocialNetworks
{
    public record UpdateSocialNetworksCommand(Guid VolunteerId, List<SocialNetworkDto> SocialNetworksDto);
}
