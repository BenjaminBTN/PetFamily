using PetFamily.Application.VolunteersManagement.Dtos;
using System;
using System.Collections.Generic;

namespace PetFamily.Application.VolunteersManagement.Update.Requsites
{
    public record UpdateRequsitesCommand(Guid VolunteerId, List<RequsiteDto> RequsitesDto);
}
