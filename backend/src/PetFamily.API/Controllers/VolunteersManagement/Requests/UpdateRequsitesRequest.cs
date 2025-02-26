﻿using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteersManagement.Commands.Update.Requsites;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests;

public record UpdateRequsitesRequest(List<VolunteerRequisiteDto> RequsitesDto)
{
    public UpdateRequsitesCommand ToCommand(Guid id)
    {
        return new(id, RequsitesDto);
    }
}
