using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers
{
    public record Email
    {
        private Email(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<Email> Create(string value)
        {
            if(string.IsNullOrWhiteSpace(value) || !value.Equals("@"))
                return Result.Failure<Email>("Email can not be empty and without @");

            return new Email(value);
        }
    }
}