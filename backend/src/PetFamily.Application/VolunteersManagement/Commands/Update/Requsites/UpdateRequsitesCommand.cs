using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;
using System;
using System.Collections.Generic;

namespace PetFamily.Application.VolunteersManagement.Commands.Update.Requsites
{
    public record UpdateRequsitesCommand(Guid VolunteerId, List<VolunteerRequsiteDto> RequsitesDto) : ICommand;
}
