using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

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
            if(Regex.IsMatch(value, @"^(\S+)@(\S+)\.(\w+)$"))
                return Result.Failure<Email>("Email is incorrect");

            return new Email(value);
        }
    }
}