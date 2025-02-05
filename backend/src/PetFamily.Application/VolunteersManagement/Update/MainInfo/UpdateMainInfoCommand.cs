﻿using PetFamily.Application.VolunteersManagement.Dtos;
using System;

namespace PetFamily.Application.VolunteersManagement.Update.MainInfo
{
    public record UpdateMainInfoCommand(
        Guid VolunteerId,
        FullNameDto FullNameDto,
        string Description,
        string Email,
        double Experience,
        string PhoneNumber);
}
