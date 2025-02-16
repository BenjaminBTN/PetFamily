using System;

namespace PetFamily.Application.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public FullNameDto FullName { get; init; } = null!;
    public string Description { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public double Experience { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public RequsiteDto[] Requisites { get; init; } = null!;
    public SocialNetworkDto[] Networks { get; init; } = null!;
}
