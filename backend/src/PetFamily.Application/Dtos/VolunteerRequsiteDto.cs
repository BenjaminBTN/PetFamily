namespace PetFamily.Application.Dtos;

public class VolunteerRequsiteDto(string name, string description)
{
    public string Name { get; init; } = name;
    public string Description { get; init; } = description;
}
