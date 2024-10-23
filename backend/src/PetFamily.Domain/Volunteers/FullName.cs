using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers
{
    public record FullName
    {
        private FullName(string name, string surname, string patronymic)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Patronymic { get; }

        public static Result<FullName> Create(string name, string surname, string patronymic)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Result.Failure<FullName>("Name can not be empty");

            if(string.IsNullOrWhiteSpace(surname))
                return Result.Failure<FullName>("Surname can not be empty");

            if(string.IsNullOrWhiteSpace(patronymic))
                return Result.Failure<FullName>("Patronymic can not be empty");

            return new FullName(name, surname, patronymic);
        }
    }
}