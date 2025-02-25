using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteersManagement.VO;

public record PetName
{
    public string Value { get; }

    private PetName(string value) => Value = value;

    public static Result<PetName, Error> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            return Errors.General.InvalidValue("PetName");

        if(value.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.OverMaxLength("PetName");

        return new PetName(value);
    }
}
