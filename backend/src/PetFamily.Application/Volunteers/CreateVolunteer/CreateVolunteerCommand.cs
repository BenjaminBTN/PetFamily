﻿using PetFamily.Application.Volunteers.Dtos;

namespace PetFamily.Application.Volunteers.CreateVolunteer
{
    public record CreateVolunteerCommand(
        FullNameDto FullName, 
        string Description, 
        string Email, 
        int Experience, 
        string PhoneNumber);
}