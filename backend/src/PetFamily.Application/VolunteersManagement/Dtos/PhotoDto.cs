namespace PetFamily.Application.VolunteersManagement.Dtos
{
    public class PhotoDto(string name, string description)
    {
        public string Name { get; init;  } = name;
        public string Description { get; init; } = description;
    }
}
