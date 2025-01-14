﻿using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain.VolunteersManagement.VO;
using System.Collections.Generic;

namespace PetFamily.Application.Volunteers.Update.Requsites
{
    public record UpdateRequsitesCommand(VolunteerId VolunteerId, List<RequsiteDto> RequsiteDtos);
}
