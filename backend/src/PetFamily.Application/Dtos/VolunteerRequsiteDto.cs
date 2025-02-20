namespace PetFamily.Application.Dtos;

public class VolunteerRequisiteDto(string name, string description)
{
    public string Name { get; init; } = name;
    public string Description { get; init; } = description;
}
