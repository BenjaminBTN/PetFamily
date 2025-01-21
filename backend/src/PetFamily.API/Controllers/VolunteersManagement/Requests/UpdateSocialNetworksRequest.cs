using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Application.VolunteersManagement.Update.SocialNetworks;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
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
