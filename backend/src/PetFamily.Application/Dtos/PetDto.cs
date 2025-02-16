using System;

namespace PetFamily.Application.Dtos;

public class PetDto
{
    public Guid Id { get; init; }
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string TypeInfo { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public string HealthInfo { get; private set; } = default!;
    // public AddressDto Address { get; private set; } = default!;
    public double Weight { get; private set; } = default;
    public double Height { get; private set; } = default;
    public string PhoneNumber { get; private set; } = default!;
    public bool IsCastrated { get; private set; } = default;
    public bool IsVaccinated { get; private set; } = default;
    public string BirthDate { get; private set; } = default!;
    public string Status { get; private set; } = default!;
    public int OrdinalNumber { get; private set; } = default!;
    // public RequisiteForHelpDto[] RequisitesForHelp { get; private set; } = null!;
    // public PhotoDto[] PetPhotos { get; private set; } = [];
}
