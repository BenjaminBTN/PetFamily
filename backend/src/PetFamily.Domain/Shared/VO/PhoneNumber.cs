using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared.VO;

public record PhoneNumber
{
    public string Value { get; }

    private PhoneNumber(string value) => Value = value;

    public static Result<PhoneNumber, Error> Create(string value)
    {
        if(!Regex.IsMatch(value, @"^\+?[1-9][0-9]{10}$"))
            return Errors.General.InvalidValue("Phone number");

        return new PhoneNumber(value);
    }
}