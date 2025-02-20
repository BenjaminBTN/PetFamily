using System;

namespace PetFamily.Application.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public FullNameDto FullName { get; set; } = null!;
    public string Description { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public double Experience { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public VolunteerRequisiteDto[] Requisites { get; set; } = null!;
    public SocialNetworkDto[] Networks { get; set; } = null!;
}
