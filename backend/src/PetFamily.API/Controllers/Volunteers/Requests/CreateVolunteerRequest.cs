﻿namespace PetFamily.API.Controllers.Volunteers.Requests
{
    public record CreateVolunteerRequest(
        string Name, 
        string Surname, 
        string Patronymic,
        string Description, 
        string Email, 
        int Experience, 
        string PhoneNumber);
}
