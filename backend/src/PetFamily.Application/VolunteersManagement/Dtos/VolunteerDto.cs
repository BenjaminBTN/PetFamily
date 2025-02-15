using System;

namespace PetFamily.Application.VolunteersManagement.Dtos
{
    public class VolunteerDto
    {
        public Guid Id { get; init; }
        public FullNameDto FullName { get; init; } = null!;
        public string Description { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public double Experience { get; init; }
        public string PhoneNumber { get; init; } = string.Empty;
        // public string Requisites { get; init; } = string.Empty;
        // public string Networks { get; init; } = string.Empty;
    }
        // public PetDto[] Pets { get; init; } = [];
}
