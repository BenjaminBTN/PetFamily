using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers
{
    public record FullName
    {
        public string Name { get; }
        public string Surname { get; }
        public string Patronymic { get; }

        private FullName(string name, string surname, string patronymic)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public static Result<FullName, Error> Create(string name, string surname, string patronymic)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Errors.General.InvalidValue("Name");

            if(string.IsNullOrWhiteSpace(surname))
                return Errors.General.InvalidValue("Surname");

            if(string.IsNullOrWhiteSpace(patronymic))
                return Errors.General.InvalidValue("Patronymic");

            return new FullName(name, surname, patronymic);
        }
    }
}