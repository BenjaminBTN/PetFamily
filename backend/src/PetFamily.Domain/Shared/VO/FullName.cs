using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.VO;

public record FullName
{
    public string Name { get; }
    public string Surname { get; }
    public string? Patronymic { get; }

    private FullName(string name, string surname, string? patronymic)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
    }

    public static Result<FullName, Error> Create(string name, string surname, string? patronymic)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Errors.General.InvalidValue("Name");

        if(string.IsNullOrWhiteSpace(surname))
            return Errors.General.InvalidValue("Surname");

        if(string.IsNullOrWhiteSpace(patronymic))
            patronymic = null;

        return new FullName(name, surname, patronymic);
    }
}