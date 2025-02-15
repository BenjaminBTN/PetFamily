namespace PetFamily.Application.VolunteersManagement.Dtos
{
    public class FullNameDto(string name, string surname, string patronymic)
    {
        public string Name { get; init; } = name;
        public string Surname { get; init; } = surname;
        public string Patronymic { get; init; } = patronymic;
    }
}
