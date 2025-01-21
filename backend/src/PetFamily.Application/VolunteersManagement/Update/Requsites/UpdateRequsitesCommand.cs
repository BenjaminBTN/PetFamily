using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Domain.VolunteersManagement.VO;
using System.Collections.Generic;

namespace PetFamily.Application.VolunteersManagement.Update.Requsites
{
    public record UpdateRequsitesCommand(VolunteerId VolunteerId, List<RequsiteDto> RequsiteDtos);
}
